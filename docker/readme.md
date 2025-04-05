# Run solution

solution.yml is docker compose file, that has all configuration for local development. It includes mosquitto as IoT broker and postgres for data storage.

##### Run

`docker-compose -f solution.yml -p demeter_project up -d`

# Run Mosquitto in Docker

**Mosquitto** is a lightweight MQTT broker.

mosquitto-dc.yml is docker compose file, that has configuration of Mosquitto

##### Run

`docker-compose -f mosquitto-dc.yml -p mosquitto_only up -d`

##### Publish test message from Mosquitto

1. Open cmd from container
2. Set topic parameter "-t"
3. Set messege parameter "-m"
2. Run command:

`mosquitto_pub -h localhost  -p 1883 -t "device" -m "My messege"`

Temperature message example:

`mosquitto_pub -h localhost  -p 1883 -t "device" -m '{"device_id":"100001","sensor":"DS18B20","temperature":21.87,"unit":"C","timestamp":1712062745}'`
