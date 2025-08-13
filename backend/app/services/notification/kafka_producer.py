import json
from aiokafka import AIOKafkaProducer
from app.core.config import settings

class KafkaProducer:
    def __init__(self):
        self.producer = AIOKafkaProducer(
            bootstrap_servers=settings.KAFKA_BOOTSTRAP_SERVERS,
            value_serializer=lambda v: json.dumps(v).encode('utf-8')
        )
        self._started = False

    async def start(self):
        if not self._started:
            await self.producer.start()
            self._started = True

    async def stop(self):
        if self._started:
            await self.producer.stop()
            self._started = False

    async def send_message(self, topic: str, message: dict):
        await self.start()
        await self.producer.send_and_wait(topic, message)

# Global instance
kafka_producer_instance = KafkaProducer()

async def get_kafka_producer() -> KafkaProducer:
    await kafka_producer_instance.start()
    return kafka_producer_instance

async def close_kafka_producer():
    await kafka_producer_instance.stop()
