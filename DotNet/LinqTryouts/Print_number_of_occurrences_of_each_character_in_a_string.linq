<Query Kind="Expression" />

"abc d abc".GroupBy(p => p).Where(r => r.Key != ' ').Select(q => new  {letter = q.Key, count = q.Count()})