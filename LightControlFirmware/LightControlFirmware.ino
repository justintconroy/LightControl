#include "config.h"
#include <WiFi.h>
#include <MQTT.h>

WiFiClient wifiClient;
MQTTClient client(1024);

unsigned long lastMillis = 0;

void connectToWifi()
{
  Serial.println("Connecting to WiFi...");
  Serial.print("SSID: ");
  Serial.println(WifiSsid);
  Serial.print("PW:   ");
  Serial.println(WifiPw);
  WiFi.begin(WifiSsid, WifiPw);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("Connected to WiFi");
  Serial.print("IP Address: ");
  Serial.println(WiFi.localIP());
}

void connectToMqtt()
{
  Serial.println("Connecting to MQTT...");
  Serial.print("Address: ");
  Serial.print(server);
  Serial.print(":");
  Serial.println(MQTT_PORT);

  client.begin(server, wifiClient);
  client.onMessage(handleMqttMessage);
  reconnect();
}

void reconnect()
{
  Serial.print("checking wifi...");
  while (WiFi.status() != WL_CONNECTED)
  {
    Serial.print(".");
    delay(1000);
  }

  Serial.print("\nconnecting...");
  while (!client.connect("arduino", "try", "try"))
  {
    Serial.print(".");
    delay(1000);
  }

  Serial.println("\nconnected!");

  client.subscribe("strand/1");
  // client.unsubscribe("/hello");
}

void handleMqttMessage(String &topic, String &payload)
{
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  Serial.println(payload);
}

void setup()
{
  Serial.begin(115200);
  Serial.println();
  Serial.println();

  connectToWifi();
  connectToMqtt();
  delay(1500);
}

void loop()
{
  client.loop();
  if (!client.connected())
  {
    reconnect();
  }
  // publish a message roughly every second.
  if (millis() - lastMillis > 1000)
  {
    lastMillis = millis();
    client.publish("strand/1/out", "worldxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii");
  }
}
