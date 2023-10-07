const express = require('express');
const qrcode = require('qrcode-terminal');
const bodyParser = require('body-parser');
const app = express();
const { Client, MessageMedia, LocalAuth   } = require('whatsapp-web.js');

// Path where the session data will be stored

const DEFAULT_HTTP_PORT = 3000


app.set('port', process.env.PORT || DEFAULT_HTTP_PORT);
app.use(bodyParser.json({limit: '50mb'}))



// Use the saved values
const client = new Client({
  authStrategy: new LocalAuth()
});



client.on('qr', (qr) => {
  //qrcode.generate(qr, {small: true});
})


client.on('authenticated', (session) => {
  console.log('authenticated');
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

client.on('message', async (msg) => {
  console.log(`on message`);
  //console.log(`on message @${JSON.stringify(msg)}`);
  if(msg.body === '!edu') {
      const chat = await client.getChatById('59178002823-1422484064@g.us')
      //Log chat
      console.log(`Chat id @${JSON.stringify(chat)}`);
      return;
      let text = "";
      let mentions = [];

      for(let participant of chat.participants) {
          //const contact = await client.getContactById(participant.id._serialized);
          
          mentions.push(participant.id._serialized);
          text += `@${participant.id.user} `;
      }

      await chat.sendMessage(text, { mentions });
  }
});

// Iniciar el cliente de WhatsApp
client.initialize();

// Iniciar el servidor Express
app.listen(app.get('port'), () => {
  console.log(`Servidor iniciado en http://localhost:${app.get('port')}`);
});