public class Currency
{
    public string iso3 { get; set; }
    public string name { get; set; }
    public int unit { get; set; }
}

public class Data
{
    public List<Payload> payload { get; set; }
}

public class Errors
{
    public object validation { get; set; }
}

public class Links
{
    public object prev { get; set; }
    public object next { get; set; }
}

public class Pagination
{
    public int page { get; set; }
    public int pages { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public Links links { get; set; }
}

public class Params
{
    public object date { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public object post_type { get; set; }
    public string per_page { get; set; }
    public string page { get; set; }
    public object slug { get; set; }
    public object q { get; set; }
}

public class Payload
{
    public string date { get; set; }
    public string published_on { get; set; }
    public string modified_on { get; set; }
    public List<Rate> rates { get; set; }
}

public class Rate
{
    public Currency currency { get; set; }
    public string buy { get; set; }
    public string sell { get; set; }
}

public class CurrencyExchangeModel
{
    public Status status { get; set; }
    public Errors errors { get; set; }
    public Params @params { get; set; }
    public Data data { get; set; }
    public Pagination pagination { get; set; }
}

public class Status
{
    public int code { get; set; }
}

