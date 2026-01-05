-- Phase I: Seed 50 Film Communities for Outreach AI
-- Run: sqlcmd -S "localhost,1433" -d Route4MoviePlug -U sa -P "Route4Dev123!" -i "Data\SeedOutreachCommunities.sql"

-- Film Commissions (10)
INSERT INTO OutreachCommunities (Id, Name, Type, Channel, Website, ContactEmail, LocationsJson, EstimatedReach, PostingRules, RequiresApproval, HasCaptcha, IsActive, TotalOutreachAttempts, SuccessfulConversions, ConversionRate, CreatedAt, UpdatedAt)
VALUES
(NEWID(), 'Greater Cleveland Film Commission', 'FilmCommission', 'Script', 'https://clevelandfilm.com', 'info@clevelandfilm.com', '["Ohio","Cleveland"]', 5000, 'Free casting/crew posts. 48hr review.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Michigan Film Office', 'FilmCommission', 'Script', 'https://www.michigan.org/film', 'info@michiganfilm.org', '["Michigan","Detroit"]', 8000, 'State resource. No spam.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Film Pennsylvania', 'FilmCommission', 'Script', 'https://filmpagrants.com', 'info@filmpagrants.com', '["Pennsylvania","Pittsburgh"]', 6000, 'Email submissions. Tax credits.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'NYC Mayor Office of Film', 'FilmCommission', 'Script', 'https://www1.nyc.gov/site/mome', 'film@mome.nyc.gov', '["New York","NYC"]', 15000, 'Official NYC board.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'California Film Commission', 'FilmCommission', 'Script', 'https://film.ca.gov', 'filmjobs@film.ca.gov', '["California","Los Angeles"]', 20000, 'Tax credit program.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Film Cincinnati', 'FilmCommission', 'Script', 'https://filmsincinnati.com', 'info@filmsincinnati.com', '["Ohio","Cincinnati"]', 3000, 'Regional office. Free posts.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'New Orleans Film Office', 'FilmCommission', 'Script', 'https://filmneworleans.org', 'info@filmneworleans.org', '["Louisiana","New Orleans"]', 7000, 'Tax incentives. Crew board.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Georgia Film Office', 'FilmCommission', 'Script', 'https://georgia.org/film', 'film@georgia.org', '["Georgia","Atlanta"]', 12000, 'Top tax credit state.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Austin Film Commission', 'FilmCommission', 'Script', 'https://austintexas.gov/film', 'film@austintexas.gov', '["Texas","Austin"]', 5500, 'City permitting. Free board.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Seattle Office of Film', 'FilmCommission', 'Script', 'https://www.seattle.gov/film', 'film@seattle.gov', '["Washington","Seattle"]', 4500, 'PNW productions.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE());

-- Forums (10)
INSERT INTO OutreachCommunities (Id, Name, Type, Channel, Website, ContactEmail, EstimatedReach, PostingRules, RequiresApproval, HasCaptcha, IsActive, TotalOutreachAttempts, SuccessfulConversions, ConversionRate, CreatedAt, UpdatedAt)
VALUES
(NEWID(), 'Backstage Forum', 'Forum', 'Script', 'https://www.backstage.com/casting', 'support@backstage.com', 50000, 'Paid platform. Premium listings.', 0, 1, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Stage 32 Forum', 'Forum', 'Script', 'https://www.stage32.com', 'support@stage32.com', 80000, 'Professional network. Free posts.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'FilmFreeway Forum', 'Forum', 'Script', 'https://filmfreeway.com', 'support@filmfreeway.com', 60000, 'Festival submissions. Job board.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'IndieWire Forum', 'Forum', 'Script', 'https://www.indiewire.com', 'classifieds@indiewire.com', 100000, 'Industry news. Classifieds.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Mandy Network', 'Forum', 'Script', 'https://www.mandy.com', 'support@mandy.com', 120000, 'Global crew/casting. Paid posts.', 0, 1, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'ProductionHUB', 'Forum', 'Script', 'https://www.productionhub.com', 'support@productionhub.com', 75000, 'Equipment/crew marketplace.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Shooting People', 'Forum', 'Script', 'https://shootingpeople.org', 'support@shootingpeople.org', 45000, 'UK indie community. Free tier.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Film Independent Forum', 'Forum', 'Script', 'https://www.filmindependent.org', 'forum@filmindependent.org', 30000, 'LA-based nonprofit. Member posts.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'No Film School Forum', 'Forum', 'Script', 'https://nofilmschool.com/forum', 'forum@nofilmschool.com', 200000, 'Education/discussion. Moderated.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Creativehead Forum', 'Forum', 'Script', 'https://www.creativehead.net', 'forum@creativehead.net', 25000, 'UK hair/makeup. Crew posts.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE());

-- Facebook Groups (10) 
INSERT INTO OutreachCommunities (Id, Name, Type, Channel, Website, EstimatedReach, PostingRules, RequiresApproval, HasCaptcha, IsActive, TotalOutreachAttempts, SuccessfulConversions, ConversionRate, CreatedAt, UpdatedAt)
VALUES
(NEWID(), 'Ohio Filmmakers Network', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/ohiofilmmakers', 8500, 'No spam. Paid posts flagged.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Indie Film Hustle', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/indiefilmhustle', 150000, 'Promo-free Fridays. Mod approval.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Film Crew & Casting Calls', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/filmcrewcasting', 75000, 'Casting/crew only. No vendors.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Actors Access Community', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/actorsaccess', 60000, 'Actor-focused. Audition posts OK.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'New England Film Community', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/newenglandfilm', 12000, 'Regional. Local projects only.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'LA Filmmakers Forum', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/lafilmmakers', 95000, 'LA-based. No self-promo Sundays.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'NYC Film & TV Production', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/nycfilmtv', 80000, 'NYC metro. Union/non-union.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Women in Film Network', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/womeninfilm', 50000, 'Female filmmakers. Respectful posts.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Short Film Creators', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/shortfilmnetwork', 40000, 'Short films only. Festival circuit.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Documentary Filmmakers', 'FacebookGroup', 'Auto', 'https://www.facebook.com/groups/documentaryfilms', 35000, 'Non-fiction. Grant/funding posts OK.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE());

-- Reddit Subreddits (10)
INSERT INTO OutreachCommunities (Id, Name, Type, Channel, Website, EstimatedReach, PostingRules, RequiresApproval, HasCaptcha, IsActive, TotalOutreachAttempts, SuccessfulConversions, ConversionRate, CreatedAt, UpdatedAt)
VALUES
(NEWID(), 'r/Filmmakers', 'Reddit', 'Auto', 'https://www.reddit.com/r/Filmmakers', 500000, 'No self-promo without flair. Read rules.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/acting', 'Reddit', 'Auto', 'https://www.reddit.com/r/acting', 200000, 'Casting posts allowed. No scams.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/LocationSound', 'Reddit', 'Auto', 'https://www.reddit.com/r/LocationSound', 15000, 'Sound crew. Tech/gig posts OK.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/cinematography', 'Reddit', 'Auto', 'https://www.reddit.com/r/cinematography', 300000, 'DP/camera. Crew posts in megathread.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/ProductionAssistants', 'Reddit', 'Auto', 'https://www.reddit.com/r/ProductionAssistants', 8000, 'PA jobs. Entry-level friendly.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/Cleveland', 'Reddit', 'Auto', 'https://www.reddit.com/r/Cleveland', 100000, 'Local sub. Film posts OK if relevant.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/LosAngeles', 'Reddit', 'Auto', 'https://www.reddit.com/r/LosAngeles', 700000, 'LA metro. No ads. Use job thread.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/nyc', 'Reddit', 'Auto', 'https://www.reddit.com/r/nyc', 600000, 'NYC sub. Film posts if local.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/ForHire', 'Reddit', 'Auto', 'https://www.reddit.com/r/forhire', 400000, 'Paid gigs. [HIRING] tag required.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/FilmIndustryLA', 'Reddit', 'Auto', 'https://www.reddit.com/r/FilmIndustryLA', 25000, 'LA industry. Jobs/networking.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE());

-- Discord Servers (10)
INSERT INTO OutreachCommunities (Id, Name, Type, Channel, Website, ContactEmail, EstimatedReach, PostingRules, RequiresApproval, HasCaptcha, IsActive, TotalOutreachAttempts, SuccessfulConversions, ConversionRate, CreatedAt, UpdatedAt)
VALUES
(NEWID(), 'Film Riot Community', 'Discord', 'Auto', 'https://discord.gg/filmriot', 'admin@filmriot.com', 30000, 'Verified server. #castingcalls channel.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Indie Film Hub', 'Discord', 'Auto', 'https://discord.gg/indiefilm', 'contact@indiefilmhub.com', 15000, 'Indie focus. #jobs channel.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Filmmaker Central', 'Discord', 'Auto', 'https://discord.gg/filmmakers', 'admin@filmmakercentral.com', 20000, 'All roles. #opportunities channel.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'r/Filmmakers Discord', 'Discord', 'Auto', 'https://discord.gg/filmmaking', NULL, 50000, 'Official Reddit Discord. #casting.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Ohio Film Community', 'Discord', 'Auto', 'https://discord.gg/ohiofilm', 'admin@ohiofilm.com', 2500, 'Regional. Local projects preferred.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Blender Artists Film', 'Discord', 'Auto', 'https://discord.gg/blender', 'community@blender.org', 80000, 'VFX/animation. #joboffers channel.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Voice Acting Club', 'Discord', 'Auto', 'https://discord.gg/voiceacting', 'admin@voiceactingclub.com', 25000, 'VO artists. #auditions channel.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Music for Film & TV', 'Discord', 'Auto', 'https://discord.gg/musicforfilm', 'contact@musicforfilm.com', 12000, 'Composers. #collaborations channel.', 0, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Short Film Hub', 'Discord', 'Auto', 'https://discord.gg/shortfilms', NULL, 8000, 'Short films. #castingcalls channel.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE()),
(NEWID(), 'Women in Film Discord', 'Discord', 'Auto', 'https://discord.gg/womeninfilm', 'community@wif.org', 10000, 'Female filmmakers. #opportunities.', 1, 0, 1, 0, 0, 0.0, GETUTCDATE(), GETUTCDATE());

GO

SELECT COUNT(*) AS TotalCommunities, 
       SUM(CASE WHEN Channel = 'Auto' THEN 1 ELSE 0 END) AS AutoChannel,
       SUM(CASE WHEN Channel = 'Script' THEN 1 ELSE 0 END) AS ScriptChannel
FROM OutreachCommunities;
GO
