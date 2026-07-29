#Current Zone Object

```c#

{
    "id": "route_1",
    "encounterGroups": {
        "grass": {
            "encounters": [
                { "pokedexId": 25, "weight": 10 },
                { "pokedexId": 4, "weight": 5 },
                { "pokedexId": 7, "weight": 20 }
            ],
        "totalWeight": 35
        }
    }
}

```

#Desired Zone Object

```c#

{
  "id":"route_1",
  "encounterGroups":{
    "1-1":{
      "totalWeight":35,
      "pokemonMinLevel":2,
      "pokemonMaxLevel":5,
      "zoneDropsBonus":[
        {
          "itemId":156,
          "weight":12
        }
      ],
      "zoneDropBase":[{"itemId":123}],
      "encounters":[
        {
          "pokedexId":25,
          "weight":10,
          "pokemonDrops":[
            {
              "itemId":12,
              "weight":70
            }
          ]
        },
        {
          "pokedexId":4,
          "weight":5,
          "pokemonDrops":[
            {
              "itemId":167,
              "weight":5
            }
          ]
        },
        {
          "pokedexId":7,
          "weight":20,
          "pokemonDrops":[
            {
              "itemId":156,
              "weight":12
            }
          ]
        }
      ],
    }
  }
}

```
