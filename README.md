# DWQueue - Distributed Event-Driven Leave Approval System 🚀

DWQueue is an enterprise-grade, highly scalable, and decoupled distributed system built with **.NET 8 Core**, **RabbitMQ**, **Redis**, and **Docker**. It leverages an Event-Driven Architecture (EDA) alongside distributed caching and robust stateless security workflows to handle employee leave requests and corporate workflows seamlessly.

---

## 🏛️ Architecture Overview

The system is architected into independent, autonomous microservices and high-performance infrastructure components that communicate asynchronously and manage data lifecycle efficiently:

* **DWQueueAPI (Publisher & Gateway):** A RESTful API built on Clean Architecture principles. It serves client requests, handles database transactions, orchestrates distributed caching, and implements advanced security middleware. When a leave request is processed, it publishes a `LeaveApprovedEvent` to RabbitMQ and instantly responds to the client.
* **Redis (In-Memory Data Store):** Functions as a high-speed multi-purpose tier. It handles **Distributed Caching** for heavy database entities and acts as a reactive storage layer for the **Token Blacklist workflow**.
* **SQL Server (Relational Database):** The source of truth, utilizing optimized schema designs and relational integrity to manage core business entities.
* **RabbitMQ (Message Broker):** Manages message exchanges and queues using an advanced Exchange-to-Exchange topology provided by **MassTransit**, ensuring bulletproof asynchronous message delivery.
* **DWNotificationService (Consumer/Worker):** An independent background worker service that listens to RabbitMQ queues. Upon consumption of events, it processes payloads and dispatches dynamic HTML email notifications.
* **MailHog (SMTP Testing Server):** A local email-testing sandbox that intercepts all outgoing emails in an intuitive Web UI, ensuring safe development environments.

---

## 🛠️ Tech Stack & Ecosystem

* **Backend Framework:** .NET 8.0 Web API & Background Workers (C#)
* **Message Broker:** RabbitMQ via **MassTransit** (AMQP 0-9-1)
* **Caching & Security Storage:** Redis (`IDistributedCache`)
* **Database Management:** SQL Server (Advanced T-SQL & Indexing optimization)
* **Object Mapping:** AutoMapper (Clean DTO & Entity separation)
* **Containerization & DevOps:** Fully Dockerized via Docker Compose with Linux-based configurations (**LPIC-1** compliant structures)
* **API Documentation:** Swagger UI with integrated JWT Authorization locks

---

## ✨ Advanced Features & Implementations

### 1. Asynchronous Event-Driven Decoupling 📨
* Utilizes **MassTransit with RabbitMQ** to handle high-throughput workloads (like leave approvals and notifications) entirely out-of-band, eliminating HTTP blocking and ensuring high system availability.

### 2. Multi-Tier Distributed Caching & DTO Optimization 🏎️
* **Sub-Millisecond Read Layers:** Frequently read, slow-changing database entities (e.g., `Departments`) are cached directly in Redis as serialized DTOs. This completely bypasses both the database querying overhead and the AutoMapper execution time on repetitive requests.
* **Smart Cache Invalidation:** Implements strict active cache removal (`_cache.RemoveAsync`) on all `Write` operations (`Create`, `Update`, `Delete`) ensuring clients never receive stale data.

### 3. Stateless Token Invalidation via Redis Blacklisting 🔒
* Resolves the native limitation of JWTs (where tokens remain valid until expiration even after logout).
* **Custom TokenBlacklistMiddleware:** Upon hitting the `/logout` endpoint, the token is extracted and pushed to Redis with an exact Time-To-Live (TTL) matching its remaining natural lifespan. The middleware intercepts incoming requests, cross-references Redis, and immediately blocks blacklisted tokens.

### 4. Enterprise-Grade Resource & Thread Management 🧩
* Full propagation of **`CancellationToken`** across all application layers (Controllers ➡️ Services ➡️ Repositories ➡️ Redis ➡️ Database operations). This guarantees that if a client aborts an HTTP request, all heavy database and infrastructure threads terminate instantly, preventing resource leakages.

---

## 🏗️ Getting Started & Installation

### Prerequisites
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (Optional, for local IDE development)

### One-Command Infrastructure Setup
The entire multi-container ecosystem is fully containerized. To spin up the databases, brokers, caching layers, and services, run:

```bash
# Clone the repository
git clone [https://github.com/your-username/DWQueueAPI-New.git](https://github.com/your-username/DWQueueAPI-New.git)
cd DWQueueAPI-New

# Spin up the infrastructure and services
docker compose up -d --build







+-------------------+
                      |   Client/Swagger  |
                      +---------+---------+
                                | (HTTP Requests)
                                v
                      +---------+---------+
                      |    DWQueueAPI     | <--- [TokenBlacklistMiddleware]
                      +----+----+----+----+
                           |    |    |
          +----------------+    |    +----------------+
          | (Cache/Tokens)      | (SQL Queries)       | (Publish Events)
          v                     v                     v
   +------+------+       +------+------+       +------+------+
   |    Redis    |       | SQL Server  |       |  RabbitMQ   |
   +-------------+       +-------------+       +------+------+
                                                      |
                                                      | (Consume Events)
                                                      v
                                               +------+------+
                                               | Worker Serv |
                                               +------+------+
                                                      | (SMTP)
                                                      v
                                               +------+------+
                                               |   MailHog   |
                                               +-------------+
