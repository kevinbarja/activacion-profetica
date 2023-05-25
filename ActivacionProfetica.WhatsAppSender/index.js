const express = require('express');
const qrcode = require('qrcode-terminal');
const bodyParser = require('body-parser');
const app = express();
const { Client, MessageMedia } = require('whatsapp-web.js');
// Configuración de Express
app.set('port', process.env.PORT || 3000);

app.use(bodyParser.json({limit: '50mb'}))
// Configuración del cliente de WhatsApp

const client = new Client();

  client.on('qr', (qr) => {
    qrcode.generate(qr, {small: true});
  })

// Evento al iniciar sesión
client.on('authenticated', (session) => {
  console.log('Cliente autenticado');
  // Puedes guardar la sesión en una base de datos o en disco si deseas mantener la sesión entre reinicios del servidor
  // Guardar la sesión: fs.writeFileSync('session.json', JSON.stringify(session));
});

// Ruta para enviar mensajes
app.post('/webhook', (req, res) => {
  const { from_number, message, ...media } = req.body;
  if (media.hasOwnProperty('imagen')){
      // Enviar el mensaje
    const mediaMessage = new MessageMedia('image/png',media.imagen, message);
    client.sendMessage(from_number, mediaMessage, {caption : message}).then(() => {
      res.send('Mensaje enviado');
    }).catch((error) => {
      res.status(500).send(`Error al enviar el mensaje: ${error}`);
    });
  }
  if(media.hasOwnProperty('documento')){
      // Enviar el mensaje
    const mediaMessage = new MessageMedia('application/pdf', media.documento, message);
    client.sendMessage(from_number, mediaMessage, {caption : message, sendMediaAsDocument: true}).then(() => {
      res.send('Mensaje enviado');
    }).catch((error) => {
      res.status(500).send(`Error al enviar el mensaje: ${error}`);
    });
  }
  if(message != "" && media === {} ){
    client.sendMessage(from_number, message).then(() => {
      res.send('Mensaje enviado');
    }).catch((error) => {
      res.status(500).send(`Error al enviar el mensaje: ${error}`);
    });
  }
});

// Iniciar el cliente de WhatsApp
client.initialize();

// Iniciar el servidor Express
app.listen(app.get('port'), () => {
  console.log(`Servidor iniciado en http://localhost:${app.get('port')}`);
});
