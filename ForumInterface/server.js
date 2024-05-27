const express = require('express');
const path = require('path');
const app = express();

const data = require('../settings.json' );

//Initialize server for html and js. Open all files in the folders:
app.use(express.static(path.join(__dirname, 'page')));
app.use(express.static(__dirname))

app.listen(data.defaultPort, () => {
  console.log(`App listening on port ${data.defaultPort}`);
});