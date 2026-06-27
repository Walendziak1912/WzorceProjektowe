następca klasycznego switch-case, ale pełnoprawne wyrażenie, które zwraca wynik. 
W praktyce często zastępuje:

string description;

if (status == OrderStatus.Paid)
{
    description = "Opłacone";
}
else if (status == OrderStatus.Shipped)
{
    description = "Wysłane";
}
else
{
    description = "Nieznany status";
}

jedną deklaratywną instrukcją:

var description = status switch
{
    OrderStatus.Paid => "Opłacone",
    OrderStatus.Shipped => "Wysłane",
    _ => "Nieznany status"
};