# Run solution

solution.yml is docker compose file, that has all configuration for local development. It includes mosquitto as IoT broker and postgres for data storage.

## Run

`docker-compose -f solution.yml -p demeter_project up -d`

# Run Mosquitto in Docker

**Mosquitto** is a lightweight MQTT broker.

mosquitto-dc.yml is docker compose file, that has configuration of Mosquitto

## Run

`docker-compose -f mosquitto-dc.yml -p mosquitto_only up -d`
