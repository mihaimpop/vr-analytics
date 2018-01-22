const bodyParser = require('body-parser');
const express = require('express');
const mongoClient = require('mongodb').MongoClient;

const app = express();

const url = 'mongodb://localhost:27017/vranalytics';

const findData = (db, req, callback) => {

  const collection = db.collection('dataset125');

  let query = req.query;
  let floatInfluence;

  const inArray = (string) => {
    let numbersArray = JSON.parse("[" + string + "]");
    let floatNumbersArray = [];
    numbersArray.forEach((element) => floatNumbersArray.push(parseFloat(element)));
    return floatNumbersArray;
  };

  if (query.colorIn) {
    let colors = query.colorIn.split(',');
    query = {
      color: {
        $in: colors
      }
    }
  }

  if (query.influence) {
    floatInfluence = parseFloat(query.influence);
    query.influence = floatInfluence;
  }

  if (query.influenceGt && query.influenceLt) {
    query = {
      influence: {
        $gt: parseFloat(query.influenceGt),
        $lt: parseFloat(query.influenceLt)
      }
    }
  } else if (query.influenceGt) {
    query = {
      influence: {
        $gt: parseFloat(query.influenceGt)
      }
    }
  } else if (query.influenceLt) {
    query = {
      influence: {
        $lt: parseFloat(query.influenceLt)
      }
    }
  }

  if (query.influenceIn) {
    query = {
      influence: {
        $in: inArray(query.influenceIn)
      }
    }
  }

  collection.find(query).toArray(function (err, result) {
    callback(result);
  });
};

app.use(express.static('client'));

app.use(bodyParser.json());

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html')
});

app.route('/api/data')
  .get(function (req, res) {
    mongoClient.connect(url, function (err, db) {
      findData(db, req, function (results) {
        db.close();
        res.send(results);
      })
    })
  });

app.listen(3000, function () {
  console.log('VR-Analytics on port 3000.')
});