from contextlib import asynccontextmanager
from fastapi import FastAPI
from app.api.v1.router import api_router
from app.core.config import settings
from app.services.notification.kafka_producer import get_kafka_producer, close_kafka_producer

@asynccontextmanager
async def lifespan(app: FastAPI):
    # Startup
    app.state.kafka_producer = await get_kafka_producer()
    yield
    # Shutdown
    await close_kafka_producer()

app = FastAPI(
    title=settings.PROJECT_NAME,
    openapi_url=f"{settings.API_V1_STR}/openapi.json",
    lifespan=lifespan
)

# Include the API router
app.include_router(api_router, prefix=settings.API_V1_STR)

@app.get("/")
async def root():
    return {"message": "Welcome to the Ticket System API"}
