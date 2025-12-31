# Stripe Payment Integration Guide

## Overview

Route4-MoviePlug uses **Stripe** for payment processing with **Stripe Connect** for multi-tenant support. Each client (e.g., Making of MARY) connects their own Stripe account, and Route4 takes an application fee cut from each transaction.

## Architecture

### Payment Flow

1. **Customer Selects Tier** → `/membership` page displays all tiers
2. **Customer Joins** → Clicks "Join Now" → Routes to `/checkout/{tierId}`
3. **Payment Details** → Customer enters email and card info
4. **Payment Intent Created** → Backend creates Stripe PaymentIntent via Stripe Connect
5. **Card Processed** → Stripe.js handles card tokenization
6. **Payment Confirmed** → Backend confirms and records transaction
7. **Success** → Customer redirected to home page

### Multi-Tenant Money Flow

```
Customer Payment → Stripe Processing
                        ↓
        (Total Amount = $100)
                        ↓
    ┌───────────────────┴─────────────────┐
    ↓                                       ↓
Client Account                      Route4 Cut (5-10%)
$95 to Making of MARY              $5-10 Application Fee
```

## Setup Instructions

### 1. Get Stripe API Keys

1. Go to [stripe.com](https://stripe.com)
2. Sign up or login to your Stripe account
3. Navigate to **Developers** → **API Keys**
4. Copy:
   - **Secret Key** (starts with `sk_test_` or `sk_live_`)
   - **Publishable Key** (starts with `pk_test_` or `pk_live_`)

### 2. Configure Environment Variables

Update `appsettings.json` with your Stripe keys:

```json
{
  "Stripe": {
    "SecretKey": "sk_test_YOUR_KEY_HERE",
    "PublishableKey": "pk_test_YOUR_KEY_HERE",
    "WebhookSecret": "whsec_YOUR_WEBHOOK_SECRET",
    "ClientId": "ca_YOUR_CLIENT_ID"
  }
}
```

### 3. Set Publishable Key in Angular

Update `ClientApp/src/app/services/payment.service.ts`:

```typescript
private readonly STRIPE_PUBLISHABLE_KEY = 'pk_test_YOUR_PUBLISHABLE_KEY';
```

### 4. Set Client Stripe Account

Add to database seed data in `Route4DbContext.cs`:

```csharp
var stripeAccount = new StripeAccount
{
    Id = Guid.NewGuid(),
    ClientId = makingOfMaryId,
    StripeAccountId = "acct_YOUR_STRIPE_CONNECT_ACCOUNT_ID",
    ApplicationFeePercent = 5m, // 5% cut
    IsVerified = true,
    IsActive = true,
    ConnectedAt = DateTime.UtcNow
};
modelBuilder.Entity<StripeAccount>().HasData(stripeAccount);
```

## API Endpoints

### Create Payment Intent
```
POST /api/clients/{clientSlug}/payments/create-intent
Content-Type: application/json

{
  "membershipTierId": "tier-uuid",
  "customerEmail": "customer@example.com",
  "customerName": "John Doe"
}

Response:
{
  "clientSecret": "pi_xxx_secret_xxx",
  "paymentIntentId": "pi_xxx",
  "amount": 149.00,
  "currency": "USD"
}
```

### Confirm Payment
```
POST /api/clients/{clientSlug}/payments/confirm
Content-Type: application/json

{
  "paymentIntentId": "pi_xxx"
}

Response:
{
  "success": true,
  "message": "Payment confirmed",
  "payment": {
    "id": "guid",
    "amount": 149.00,
    "status": "completed",
    "completedAt": "2025-12-31T18:00:00Z"
  }
}
```

### Get Payment Status
```
GET /api/clients/{clientSlug}/payments/status/{paymentIntentId}

Response:
{
  "id": "guid",
  "stripePaymentIntentId": "pi_xxx",
  "amount": 149.00,
  "status": "completed",
  "customerEmail": "customer@example.com",
  "createdAt": "2025-12-31T18:00:00Z",
  "completedAt": "2025-12-31T18:02:00Z"
}
```

### Get Stripe Account Status
```
GET /api/clients/{clientSlug}/payments/stripe-account

Response:
{
  "configured": true,
  "verified": true,
  "applicationFeePercent": 5,
  "connectedAt": "2025-12-31T12:00:00Z"
}
```

## Testing

### Test Mode (Sandbox)

Use Stripe test cards to process payments in sandbox:

**Successful Payment:**
- Card: `4242 4242 4242 4242`
- Expiry: `12/25`
- CVC: `123`

**Declined Card:**
- Card: `4000 0000 0000 0002`
- Expiry: `12/25`
- CVC: `123`

### Test Workflow

1. Start both servers:
   ```bash
   # Terminal 1: Backend
   cd C:\Users\rodne\Src\route4-movieplug
   dotnet run

   # Terminal 2: Frontend
   cd C:\Users\rodne\Src\route4-movieplug\ClientApp
   npm start
   ```

2. Navigate to http://localhost:4200/membership
3. Click "Join Now" on any tier
4. Enter email and customer name
5. Use test card `4242 4242 4242 4242`
6. Complete payment

## Database Models

### Payment
```csharp
public class Payment
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid? MembershipTierId { get; set; }
    public string StripePaymentIntentId { get; set; }
    public string CustomerEmail { get; set; }
    public string? CustomerName { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } // pending, completed, failed
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
```

### StripeAccount
```csharp
public class StripeAccount
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string StripeAccountId { get; set; } // acct_xxx
    public string? RefreshToken { get; set; }
    public decimal ApplicationFeePercent { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
    public DateTime ConnectedAt { get; set; }
}
```

### Invoice
```csharp
public class Invoice
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public string StripeInvoiceId { get; set; }
    public decimal Amount { get; set; }
    public decimal PlatformFee { get; set; } // Route4's cut
    public decimal ClientAmount { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
}
```

## Frontend Components

### Payment Service
**File:** `ClientApp/src/app/services/payment.service.ts`

- `createPaymentIntent()` - Create Stripe PaymentIntent
- `confirmPayment()` - Confirm payment after card processing
- `getPaymentStatus()` - Check payment status
- `getStripeAccountStatus()` - Verify client has Stripe configured

### Checkout Component
**File:** `ClientApp/src/app/pages/checkout/checkout.component.ts`

- Displays order summary
- Collects customer info (name, email)
- Handles card tokenization
- Shows success/error states
- Stripe.js integration

## Routes

| Route | Component | Purpose |
|-------|-----------|---------|
| `/` | SplashPageComponent | Main landing page |
| `/membership` | MembershipTiersComponent | View all membership tiers |
| `/checkout/:tierId` | CheckoutComponent | Payment form |
| `/admin` | AdminDashboard | Client admin area |

## Security Considerations

✅ **Implemented:**
- PCI compliance via Stripe.js (no card data touches your server)
- HTTPS required in production
- Unique PaymentIntent per transaction
- Application fee automatically deducted
- Database records all payments

⚠️ **TODO:**
- Implement webhook signature verification for Stripe events
- Add customer authentication before checkout
- Implement refund handling
- Add payment dispute resolution
- Rate limiting on payment endpoints
- Audit logging for all payments

## Webhook Events

When payments complete, Stripe sends webhook events:

```
payment_intent.succeeded
payment_intent.payment_failed
invoice.payment_succeeded
invoice.payment_failed
charge.refunded
```

**TODO:** Implement webhook handler in `Controllers/WebhooksController.cs` to:
- Listen for `payment_intent.succeeded`
- Send confirmation email to customer
- Update membership status
- Generate invoice records

## Subscription Support

For recurring payments (Film Students, Cult Fans at $149/$179/year):

**TODO:**
- Update tiers with `subscriptionPriceId` (Stripe Price ID)
- Switch from `confirmCardPayment` to `confirmCardSetup` for subscriptions
- Store `stripeSubscriptionId` in Payment record
- Auto-renew or webhook-trigger renewal logic

## Live Mode Transition

When ready for production:

1. **Stripe Account:** Upgrade from test to live mode
2. **Live Keys:** Use `sk_live_` and `pk_live_` keys
3. **Update Config:** Change keys in `appsettings.json`
4. **HTTPS Only:** Enforce HTTPS in production
5. **Webhook Secret:** Update webhook secret for live events
6. **Testing:** Re-test with real payment methods

## Support

For Stripe issues:
- [Stripe Documentation](https://stripe.com/docs)
- [Stripe.NET Library](https://github.com/stripe/stripe-dotnet)
- [Stripe Support Dashboard](https://support.stripe.com)
