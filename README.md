# NewsSentimWebApi

Responses 

Endpoint [getMostPositiveNewsCategory] : Get the news category with best average sentiment across all news titles within that category.
Path [/NewsSentim/getMostPositiveNewsCategory]
Response 
{
   "category":"startup",
   "avgSentimentPolarity":0.06583333
}

Endpoint [getMostPositiveNewsAuthor]: Get the author with the best average sentiment across all news titles across all categories.
Path [/NewsSentim/getMostPositiveNewsAuthor]
Response 
{
   "author":"Aishwarya Awasthi",
   "avgSentimentPolarity":0.275
}

Endpoint [getTop3PositiveArticles]: Get the top 3 articles with the best sentiments in their titles sorted by the sentiment rating.
Path [/NewsSentim/getTop3PositiveArticles]
Response 
[
   {
      "title":"Neeraj Chopra trains students at Gujarat school, PM Modi tweets 'Great moments!'",
      "category":"sports",
      "author":"Apaar Sharma",
      "sentimentPolarity":1
   },
   {
      "title":"I'll look awesome: Travis to troll who claimed he'll regret his tattoos",
      "category":"entertainment",
      "author":"Mahima Kharbanda",
      "sentimentPolarity":1
   },
   {
      "title":"Tesla asks US court to affirm its win against ex-engineer: Report",
      "category":"technology",
      "author":"Aishwarya Awasthi",
      "sentimentPolarity":0.8
   }
]
