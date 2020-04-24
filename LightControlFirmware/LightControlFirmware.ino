#include <WiFi.h>
#include <PubSubClient.h>

const char *WIFI_SSID = "<WiFi SSID>";
const char *WIFI_PW = "<WiFi PW>";

IPAddress server(127, 0 ,0 ,1);
#define MQTT_PORT 1883

#define NUM_LEDS 50
#define LED_TYPE WS2801
#define COLOR_ORDER RGB
#define DATA_PIN 23
#define CLK_PIN 18
#define VOLTS 5
#define MAX_MA 4000

WiFiClient wifiClient;
PubSubClient client(wifiClient);

void connectToWifi()
{
  Serial.println("Connecting to WiFi...");
  Serial.print("SSID: ");
  Serial.println(WIFI_SSID);
  Serial.print("PW:   ");
  Serial.println(WIFI_PW);
  WiFi.begin(WIFI_SSID, WIFI_PW);
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

  while (!client.connected())
  {
    Serial.println("Attempting connection...");
    if (client.connect("arduinoClient"))
    {
      Serial.println("connected");
      client.publish("outTopic", "hello world");
      client.subscribe("inTopic");
    }
    else
    {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      delay(5000);
    }
  }
}

void handleMqttMessage(char *topic, byte *payload, unsigned int length)
{
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  for (int i = 0; i < length; i++)
  {
    Serial.print((char)payload[i]);
  }
  Serial.println();
}

void setup()
{
  Serial.begin(115200);
  Serial.println();
  Serial.println();

  client.setServer(server, MQTT_PORT);
  client.setCallback(handleMqttMessage);

  connectToWifi();
  connectToMqtt();
  delay(1500);
}

void loop()
{
  if (!client.connected())
  {
    connectToMqtt();
  }
  client.loop();
}
