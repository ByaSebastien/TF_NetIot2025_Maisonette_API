#include "WiFiEsp.h"
#include "PubSubClient.h"
#include "SoftwareSerial.h"


#define FAN_PIN 2

const char* ssid = "BSTORM-7577";
const char* password = "Test1234=";
const char* serverAddress = "10.10.5.126";

unsigned long lastMqttPublish = 0;
const long interval = 5000;
bool isActive = false;

WiFiEspClient espClient;
PubSubClient mqttClient = PubSubClient(espClient);
SoftwareSerial softserial(A9,A8);

void callback(char* topic, byte* payload, unsigned int lenght){
  Serial.print("Message reçu [");
  Serial.print(topic);
  Serial.print("] : ");

  for(int i = 0; i < lenght; i++) {
    Serial.print((char)payload[i]);
  }

  Serial.println();

  if(strcmp(topic,"maisonnette/light") == 0){
    Serial.print("Maisonnette/light reconnue");
    isActive = !isActive;
    digitalWrite(FAN_PIN, isActive ? HIGH : LOW);
  }
}

void reconnect() {
  while (!mqttClient.connected()) {
    Serial.print("Connexion au broker MQTT...");
    if (mqttClient.connect("Maisonette")) {
      Serial.println("Connecté !");
      if (mqttClient.subscribe("maisonnette/light")) {
        Serial.println("Abonnement au topic 'maisonnette/light' réussi");
      } else {
        Serial.println("Échec de l'abonnement");
      }
      // mqttClient.subscribe("maisonnette/light"); // écouter les commandes ASP.NET
    } else {
      Serial.print("Échec, rc=");
      Serial.print(mqttClient.state());
      delay(5000);
    }
  }
}

void setup() {
  Serial.begin(9600);

  pinMode(FAN_PIN,OUTPUT);

  connectWifi();

  mqttClient.setServer(serverAddress,1883);
  mqttClient.setCallback(callback);
  mqttClient.setKeepAlive(60);
}

void loop() {
  if(!mqttClient.connected()){
    reconnect();
  }
  mqttClient.loop();

  delay(100);
}

void connectWifi() {
  softserial.begin(115200);
  softserial.write("AT+CIOBAUD=9600\r\n");
  softserial.write("AT+RST\r\n");
  softserial.begin(9600);

  WiFi.init(&softserial);

  WiFi.begin(ssid,password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("WiFi connecté!");
}
