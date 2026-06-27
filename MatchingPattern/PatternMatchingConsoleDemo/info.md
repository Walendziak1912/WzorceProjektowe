Przykład, przy którym wiele osób pierwszy raz dostrzega prawdziwą siłę pattern matchingu. 
Zamiast opisywać jak dojść do wyniku (kolejne if), deklarujesz jaki obiekt Cię interesuje. 
Dzięki temu kod jest krótszy, bardziej czytelny i łatwiejszy do rozbudowy.

if(order != null)
{
    if(order.Customer != null)
    {
        if(order.Customer.IsVip)
        {
            if(order.Items.Count > 5)
            {
                if(order.Total > 5000)
                {
                    ...
                }
            }
        }
    }
}

z pattern matchingiem wygląda to tak:

return order switch
{
    {
        Customer: { IsVip: true },
        Items: { Count: > 5 },
        Total: > 5000
    } => ProcessVip(),

    _ => ProcessNormal()
};

RecursivePatternExamples ⭐⭐⭐⭐⭐
    property + list + relational razem
    bardzo złożone wzorce
    deconstruction
SwitchExpressionExamples
    praktyczne zastąpienie if/else
    mapowanie DTO
    statusy
    kalkulatory
    routing
RealWorldExamples ⭐⭐⭐⭐⭐
    walidacja zamówienia
    CQRS
    Event Sourcing
    API
    workflow
    automaty stanów