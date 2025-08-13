# Python base image
FROM python:3.11-slim

# Set working directory
WORKDIR /app

# Set environment variables
ENV PYTHONDONTWRITEBYTECODE 1
ENV PYTHONUNBUFFERED 1

# Install dependencies
COPY pyproject.toml .
RUN pip install poetry
RUN poetry config virtualenvs.create false && poetry install --no-root --no-dev

# Copy project code
COPY . .

# Expose port
EXPOSE 8000
