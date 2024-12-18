# 🛍️🛍️ PurchaseApi 🛍️🛍️

## Description
PurchaseApi is a repository for managing product purchases. It utilizes messaging to handle the states of products and their purchases. The API is containerized using Docker.

## Features
- Messaging for handling product and purchase states
- Docker containerization for the API
- Built with C#

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Docker

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/GuilhermeNono/PurchaseApi.git
   ```
2. Navigate to the project directory:
   ```bash
   cd PurchaseApi
   ```

### Building the Docker Container
1. Build the Docker image:
   ```bash
   docker build -t purchaseapi .
   ```
2. Run the Docker container:
   ```bash
   docker run -d -p 8080:8080 -p 8081:8081 purchaseapi
   ```

### Running the API
1. Navigate to the API project directory:
   ```bash
   cd PurchaseOrder.Api
   ```
2. Run the API:
   ```bash
   dotnet run
   ```

## Usage
- The API endpoints can be accessed at `http://localhost:8080` (or the configured port).
- Swagger UI is available for exploring the endpoints when running in development mode.

## Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature`)
5. Create a new Pull Request

## License
This project is licensed under the MIT License.
