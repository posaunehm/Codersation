module MoneyStockerSpecification

open NaturalSpec

open VendingMachine

let get_pay_back (stocker:MoneyStocker) =
    printMethod ()
    stocker.PayBack() 

let pay_back_money (stocker:MoneyStocker) =
    printMethod ()
    ignore (stocker.PayBack()) 
    stocker

let pay_backed (stocker:MoneyStocker) =
    printMethod ()
    ignore(stocker.PayBack() )
    stocker

let inserted moneyArr (stocker:MoneyStocker) = 
    printMethod moneyArr
    moneyArr |> Seq.iter (fun m ->  stocker.Insert(new Money(m)))
    stocker

let coins (moneyArr:seq<MoneyKind>) (money:seq<Money>) = 
    printMethod moneyArr
    money |> Seq.sortBy (fun m -> m.Kind) 
          |> Seq.forall2 (fun m1 m2 -> m1 = m2.Kind) (moneyArr |> Seq.sort)

let amount amount (stocker:MoneyStocker) = 
    printMethod amount
    stocker.InsertedMoneyAmount = amount
    

let stocked (moneyArr:seq<MoneyKind>) (stocker:MoneyStocker) = 
    printMethod moneyArr
    moneyArr |> Seq.iter (fun m -> stocker.Stock(new Money(m)))
    stocker

let used (usedAmount:int) (stocker:MoneyStocker) = 
    printMethod usedAmount
    stocker.TakeMoney(usedAmount)
    stocker


let try_use_amount_of (amount:int) (stocker:MoneyStocker) = 
    printMethod amount
    stocker.CanRetuenJustMoneyIfUsed(amount)

[<Scenario>]
let ``Given MoneyStocker Stocked 0 yen, when payback money you get 0 yen``() = 
    Given (new MoneyStocker())
        |> When get_pay_back 
        |> It should be empty
        |> Verify

[<Scenario>]
let ``Given MoneyStocker inserted one 100 yen coin, when payback money you get 100 yen coin``() = 
    Given (new MoneyStocker()) |> inserted [MoneyKind.Yen100]
        |> When get_pay_back 
        |> It should have (coins [MoneyKind.Yen100])
        |> Verify

[<Scenario>]
let ``Given MoneyStocker stocked 10 10yen coin, inserted two 100 yen coin and used 110yen, when payback money you get 10 yen coin``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 10 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> used 110
        |> When get_pay_back 
        |> It should have (coins [for i in 1 .. 9 -> MoneyKind.Yen10])
        |> Verify

[<Scenario>]
let ``After payback, It's total amount is 0 yen``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 10 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> used 110
        |> When pay_back_money
        |> It should have (amount 0)
        |> Verify

[<Scenario>]
let ``Given MoneyStocker stocked 5 10yen coin, inserted two 100 yen,  when try using 110 yen you can't``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 5 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
        |> When try_use_amount_of 110 
        |> It should equal false
        |> Verify

[<Scenario>]
let ``Given MoneyStocker stocked 10 10yen coin, inserted two 100 yen,  when try using 110yen you can``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 10 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
        |> When try_use_amount_of 110 
        |> It should equal true
        |> Verify

[<Scenario>]
let ``Given MoneyStocker stocked 10 10yen coin, inserted 300 yen, and used 110yen, you can use 110yen again``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 10 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen500]
            |> used 110
        |> When try_use_amount_of 110 
        |> It should equal false
        |> Verify

[<Scenario>]
let ``Given MoneyStocker stocked 10 10yen coin, inserted 200 yen, used 110yen, paybacked, again inserted 200yen, you can't use 110yen again``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 10 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> used 110
            |> pay_backed
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
        |> When try_use_amount_of 110 
        |> It should equal false
        |> Verify

[<Scenario>]
let ``Given MoneyStocker stocked 0 10yen coin, inserted 200 yen, used 110yen, paybacked, again inserted 200yen, you can use 110yen again``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 20 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> used 110
            |> pay_backed
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
        |> When try_use_amount_of 110 
        |> It should equal true
        |> Verify