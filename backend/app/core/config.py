from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    PROJECT_NAME: str = "Enterprise Ticket System"
    API_V1_STR: str = "/api/v1"

    # Database
    DATABASE_URL: str

    # JWT
    JWT_SECRET_KEY: str
    JWT_ALGORITHM: str = "HS256"
    ACCESS_TOKEN_EXPIRE_MINUTES: int = 60 * 24 * 8 # 8 days

    # Kafka
    KAFKA_BOOTSTRAP_SERVERS: str = "kafka:9092"
    KAFKA_TICKET_TOPIC: str = "ticket_events"

    # Redis
    REDIS_URL: str

    class Config:
        case_sensitive = True
        env_file = ".env"

settings = Settings()
