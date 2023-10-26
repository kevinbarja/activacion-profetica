const express = require('express');
const qrcode = require('qrcode-terminal');
const bodyParser = require('body-parser');
const app = express();
const { Client, MessageMedia, LocalAuth   } = require('whatsapp-web.js');
const axios = require("axios").default;
const request = require("request");
const FormData = require("form-data");
const fs = require('fs');

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

function isIterable(obj, prop) {
  return typeof obj[prop] === "object" && typeof obj[prop][Symbol.iterator] === "function";
}

client.on('message', async (msg) => {
  const chat = await msg.getChat();
  //Exit if is message group
  if (isIterable(chat, "participants")) return;
  //Exit if isn't text message
  let message = msg.body;
  if (msg.type != 'chat'){
    message = '';
  }
  //Alow only bolivian number with prefix 591
  if (!msg.from.startsWith('591') ) {
    msg.reply('Lo siento, por el momento sólo converso con números de Bolivia. 😔');
    return;
  }

  try {
    const response = await axios({
      method: "POST",
      url: `http://localhost:5103/messages`,
      data: { whatsappNumber: msg.from.replace('591','').replace('@c.us',''), message : message },
    });

    if (response.data.message != '' && msg.from != '59172103001@c.us'){
      client.sendMessage( msg.from, response.data.message);
    }

    writeToFile(JSON.stringify(response.data));
  } catch (error) {
    // Manejar el error aquí
    writeToFile('Error al hacer la solicitud:' + JSON.stringify(error));
    if (msg.from != '59172103001@c.us')
    {
      msg.reply('Lo siento, ocurrió un error no controlado. 😔');
    }
  }
});

// Iniciar el cliente de WhatsApp
client.initialize();

// Iniciar el servidor Express
app.listen(app.get('port'), () => {
  console.log(`Servidor iniciado en http://localhost:${app.get('port')}`);
});


function writeToFile(message) {
  fs.appendFile("logfile.txt", message + "\n", (err) => {
    if (err) {
      console.error('Error writing to the file:', err);
    } else {
      console.log('Data has been written to the file.');
    }
  });
}