| Section             |                                 Description                                  |       Test Text |
| :------------------ | :--------------------------------------------------------------------------: | --------------: |
| GetAllPokemonMeta   |                             string graphql query                             |  [Link](#first) |
| Current Zone Object | representation of the zone levels and stages object to be loaded into memory | [Link](#second) |
| Desired Zone Object |   desired result of iterative work on zones from **#Current Zone Object**    |  [Link](#third) |

#Current Graphql query for GetAllPokemonMeta
<a id="first"></a>

> Suppose to obtain all the metadat from pokeapi external graphql api to then store in game data
>
> To be stored on disk then loaded into memory for faster pokemon meta lookup

---

#Current Zone Object [Link To Desired](#third)
<a id="second"></a>

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

#Desired Zone Object [Link To Second](#second)
<a id="third"></a>

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
