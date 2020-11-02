// Website html stuff
const { Client } = require('pg'); 

const client = new Client({
  connectionString: process.env.DATABASE_URL,
  ssl: {
    rejectUnauthorized: false
  }
});

client.connect();

client.query('INSERT INTO testusers(email) VALUES ('testemail');');
