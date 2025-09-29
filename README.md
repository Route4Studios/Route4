# Route4 FFmpeg REST API

> A powerful REST API service for video and audio processing using FFmpeg, featuring enterprise-grade CI/CD pipelines and professional development workflows.

## 🎯 **Overview**

Route4 FFmpeg REST API is a Node.js-based service that provides HTTP endpoints for common video and audio processing tasks. Originally built to solve the challenge of managing complex FFmpeg command lines, this project has evolved into a comprehensive media processing platform with modern DevOps practices.

### **Key Features**
- 🎵 **Audio Processing**: Extract, convert, and normalize audio from video files
- 🏥 **Health Monitoring**: Comprehensive system health checks and diagnostics  
- 🐳 **Containerized**: Docker support for consistent deployment
- 🚀 **CI/CD Pipeline**: Professional GitHub Actions workflows
- 📱 **Mobile Testing**: Built-in phone testing capabilities
- 📊 **API Documentation**: Interactive Swagger/OpenAPI documentation
- 🔧 **Developer Tools**: Custom PowerShell deployment automation

## 🏗️ **Architecture & Technology Stack**

### **Application Stack**
```
Frontend: React Native/Web (Planned)
    ↓
REST API: Node.js + Express.js
    ↓  
Media Processing: FFmpeg
    ↓
Container: Docker (Linux-based)
    ↓
Deployment: AWS ECS (Future)
```

### **Development & CI/CD Stack**
```
Development: PowerShell Visual Pipeline
    ↓
Integration: GitHub Actions (YAML)
    ↓
Production: GitHub Actions → AWS
    ↓
Monitoring: Health checks + Logging
```

## 🌿 **Branching Strategy & Workflows**

### **Branch Structure**
```
master (🔴 Production)
├── Automated production deployment
├── Branch protection with required status checks
└── Triggers AWS deployment pipeline

develop (🟡 Integration)  
├── Integration testing environment
├── GitHub Actions validation
└── Merge point for feature branches

feature/* (🟢 Development)
├── Individual feature development
├── PowerShell visual development pipeline
└── Local testing and iteration
```

### **Development Workflow**
```bash
# 1. Feature Development (PowerShell Pipeline)
git checkout develop
git checkout -b feature/your-feature
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 dev

# 2. Integration Testing (GitHub Actions)
git push origin feature/your-feature
# Create PR: feature/your-feature → develop

# 3. Production Deployment (GitHub Actions)  
# Create PR: develop → master
# Automatic AWS deployment (configured)
```

## 🚀 **Quick Start**

### **Prerequisites**
- Node.js 18+ 
- Docker Desktop
- PowerShell 5.1+ or PowerShell Core
- FFmpeg installed locally (optional, for development)

### **Development Setup**
```bash
# Clone the repository
git clone [your-repo-url]
cd route4-ffmpeg-rest-api

# Install dependencies
npm install

# Start development server with visual pipeline
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 dev
```

### **Available Development Commands**
```powershell
# PowerShell Development Pipeline Commands
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 dev          # Start development server
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 test         # Run test suite  
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 phone-test   # Mobile device testing
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 local-docker # Container testing
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 status       # Service status
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 logs         # View logs
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 stop         # Stop all services
```

## 📋 **API Endpoints**

### **🏥 Health & System**
| Endpoint | Method | Description |
|----------|--------|-------------|
| `/` | GET | Basic service information |
| `/health` | GET | Health check with uptime and memory |
| `/system` | GET | Detailed system info and FFmpeg status |

### **🎵 Audio Processing** 
| Endpoint | Method | Description |
|----------|--------|-------------|
| `/audio/extractAudio` | POST | Extract audio tracks from video files |
| `/audio/convert` | POST | Convert audio between formats (MP3, WAV, FLAC, AAC, OGG) |
| `/audio/normalize` | POST | Normalize audio levels with LUFS targeting |

### **📊 Interactive Documentation**
- **Swagger UI**: `http://localhost:3000/api-docs`
- **Try endpoints directly** from the browser
- **File upload testing** for audio processing
- **Real-time API exploration**

## 🐳 **Docker & Containerization**

### **Local Container Testing**
```bash
# Build and run container locally
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 local-docker

# Access containerized service
# URL: http://localhost:[auto-assigned-port]
# API Docs: http://localhost:[auto-assigned-port]/api-docs
```

### **Container Architecture**
```dockerfile
# Linux-based container (Debian)
FROM node:18
# Your Node.js app runs on Linux in container
# Even when developed on Windows host
```

## 🔄 **CI/CD Pipelines**

### **GitHub Actions Workflows**

#### **Development Pipeline** (develop branch)
```yaml
# .github/workflows/development.yml
- Quick validation testing
- PowerShell integration checks  
- Development environment validation
```

#### **Production Pipeline** (master branch)  
```yaml
# .github/workflows/production.yml
- Comprehensive test suite
- Security scanning
- Production Docker build
- AWS deployment (configured)
```

#### **Full Deploy Pipeline** (master branch)
```yaml
# .github/workflows/full-deploy.yml  
- Production readiness validation
- Complete deployment simulation
- Health check verification
- Deployment reporting
```

### **Branch Protection Rules**
- ✅ **master**: Requires PR review + status checks
- ✅ **develop**: Requires PR review + status checks  
- ✅ **Automated testing**: Must pass before merge
- ✅ **No direct pushes**: All changes via Pull Requests

## 📱 **Mobile & Cross-Platform Testing**

### **Phone Testing Capabilities**
```powershell
# Test API on physical mobile devices
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 phone-test

# Features:
# - WiFi network access URLs
# - ngrok tunnel creation (optional)
# - Mobile-optimized API documentation
# - Real device testing without cloud deployment
```

## 🔧 **Advanced Features**

### **PowerShell Visual Pipeline**
- 🎨 **Emoji-rich status updates** for clear visual feedback
- 🌈 **Color-coded environment indicators** (🟢 Dev, 🟡 Integration, 🔴 Prod)
- ⚡ **Branch-aware operations** with automatic environment detection
- 📊 **Real-time health monitoring** and port management
- 🔄 **Auto-restart capabilities** with graceful error handling

### **Environment Detection**
```powershell
# Automatic branch detection and environment setup
🟢 [DEVELOPMENT] Current branch: feature/audio-enhancement
🟡 [INTEGRATION] Current branch: develop  
🔴 [PRODUCTION] Current branch: master
```

### **Health Monitoring**
- **System resource monitoring** (CPU, memory, disk)
- **FFmpeg availability validation**
- **Port conflict resolution**
- **Service dependency checking**
- **Automated recovery procedures**

## 🎯 **Use Cases & Applications**

### **Personal Media Library**
- Convert 360° VR footage (Insta360 Titan MP4 → MOV)
- Audio format standardization across media collections
- Batch processing of video files for editing workflows

### **Professional Video Production**
- Post-production audio extraction and processing
- Format conversion for different platforms and devices
- Quality normalization for consistent output

### **Enterprise Integration**  
- Microservice architecture for media processing
- API integration with existing video platforms
- Scalable container deployment for high-volume processing

## 🚀 **Deployment**

### **Local Development**
```bash
# Immediate development server
npm start
# OR with PowerShell pipeline
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 dev
```

### **Container Deployment**
```bash
# Local container testing
docker build -t route4-ffmpeg:latest .
docker run -p 3000:3000 route4-ffmpeg:latest

# OR with automated pipeline
.\deploy\route4-ffmpeg-rest-api-dev-deployer.ps1 local-docker
```

### **Production Deployment**
```bash
# Automated via GitHub Actions
git checkout develop
git checkout -b feature/new-feature
# ... make changes ...
git push origin feature/new-feature
# Create PR → develop → master → AWS
```

## 📊 **Project Status & Roadmap**

### **✅ Implemented Features**
- ✅ REST API foundation with Express.js
- ✅ Audio extraction and processing endpoints
- ✅ Docker containerization
- ✅ PowerShell development automation  
- ✅ GitHub Actions CI/CD pipeline
- ✅ Branch strategy and protection rules
- ✅ Swagger API documentation
- ✅ Health monitoring and system diagnostics
- ✅ Mobile device testing capabilities

### **🚧 In Development**
- 🚧 Video format conversion endpoints
- 🚧 React Native mobile application
- 🚧 Web-based user interface
- 🚧 AWS ECS production deployment
- 🚧 Advanced video editing capabilities

### **📋 Planned Features**
- 📋 Batch file processing workflows
- 📋 Video streaming format generation (HLS, DASH)
- 📋 Advanced metadata extraction and analysis
- 📋 User authentication and authorization
- 📋 File storage integration (AWS S3)
- 📋 Performance monitoring and analytics

## 🛠️ **Development Tools & Scripts**

### **PowerShell Development Suite**
```powershell
# Core development pipeline
route4-ffmpeg-rest-api-dev-deployer.ps1
├── dev           # Development server with hot reload
├── test          # Comprehensive test suite execution  
├── phone-test    # Mobile device testing setup
├── local-docker  # Container build and deployment
├── status        # System and service status checking
├── logs          # Centralized log viewing
└── stop          # Graceful service shutdown
```

### **Visual Feedback System**
- 🎨 **Color-coded status indicators** for immediate feedback
- 📊 **Real-time performance metrics** during development
- 🔔 **Notification system** for build status and errors
- 📱 **Cross-platform compatibility** testing tools

## 📚 **Documentation & Resources**

### **API Documentation**
- **Interactive Swagger UI**: Available at `/api-docs` when server is running
- **OpenAPI 3.0 specification**: Complete API schema documentation
- **Try-it-out functionality**: Test endpoints directly from browser
- **Request/response examples**: Comprehensive usage examples

### **Development Documentation**
- **PowerShell pipeline usage**: Detailed command explanations
- **Docker deployment guide**: Container setup and configuration
- **CI/CD workflow documentation**: GitHub Actions pipeline details
- **Troubleshooting guide**: Common issues and solutions

## 🤝 **Contributing**

### **Development Workflow**
1. **Fork the repository** and create feature branch from `develop`
2. **Follow branch naming**: `feature/description` or `fix/description`
3. **Use PowerShell pipeline** for local development and testing
4. **Ensure all tests pass** before creating Pull Request
5. **Update documentation** for new features or API changes
6. **Create Pull Request** to `develop` branch with clear description

### **Code Standards**
- **ESLint configuration**: Automated code quality checking
- **JSDoc documentation**: Comprehensive inline documentation
- **Test coverage**: Maintain test coverage for new features
- **API documentation**: Update Swagger specs for endpoint changes

### **Review Process**
- **Automated testing**: GitHub Actions validate all changes
- **Code review**: Required approval before merge to `develop`
- **Integration testing**: Comprehensive testing on `develop` branch
- **Production deployment**: Controlled release process via `master` branch

## 📄 **License**

This project is open source and available under the [MIT License](LICENSE).

## 🙏 **Acknowledgments**

- **FFmpeg**: The foundation of all media processing capabilities
- **Express.js**: Web framework powering the REST API
- **Docker**: Containerization platform enabling consistent deployments
- **GitHub Actions**: CI/CD platform automating our workflows
- **Node.js ecosystem**: Rich library ecosystem enabling rapid development

---

**Built with ❤️ for video professionals, developers, and media enthusiasts.**

For questions, issues, or contributions, please visit our [GitHub repository](your-repo-url) or open an issue.
