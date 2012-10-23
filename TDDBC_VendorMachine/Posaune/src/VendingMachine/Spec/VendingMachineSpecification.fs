module VendingMachineSpecification

open NaturalSpec
open VendingMachine


let initially vm =
    printMethod ()
    vm

let total_amount amount (vm:VendingMachine)  =
    printMethod amount 
    vm.TotalAmount = amount 

let insert_money amount (vm:VendingMachine) = 
    printMethod amount
    amount |> List.iter vm.InsertMoney
    vm

let cola = new Drink("Cola")
let soda = new Drink("Soda")


let stocked (moneyArr:List<MoneyKind>) (vm:VendingMachine) = 
    vm.AddStock(moneyArr |> Seq.map (fun m -> new Money(m))) 
    vm

let standardStock = 
    [
        for i in 1 .. 10 do 
            yield MoneyKind.Yen10
            yield MoneyKind.Yen50
            yield MoneyKind.Yen100
            yield MoneyKind.Yen500
            ]

let inserted (moneyArr:List<MoneyKind>) (vm:VendingMachine) = 
    printMethod moneyArr
    moneyArr |> List.iter (fun x -> vm.InsertMoney(new Money(x)))
    vm


let pushed (drinkArr:List<Drink>) (vm:VendingMachine) = 
    printMethod drinkArr
    vm.AddDrink(drinkArr)
    vm
    
let buy_drink_named (drinkName:string) (vm:VendingMachine) =
    printMethod drinkName
    vm.BuyDrink(drinkName)

let drink_named name (drink:Drink) =
    printMethod name
    drink.Name = name
 
let bought (drinkArr:List<string>) (vm:VendingMachine) = 
    drinkArr |> List.iter (fun x -> ignore (vm.BuyDrink(x)))
    vm

let coin_50_yen_for length (moneySeq:seq<Money>) = 
    printMethod length
    let count = moneySeq |> Seq.filter (fun x -> x.Kind = MoneyKind.Yen50) |> Seq.length
    count = length

let coin_10_yen_for length (moneySeq:seq<Money>) = 
    printMethod length
    let count = moneySeq |> Seq.filter (fun x -> x.Kind = MoneyKind.Yen10) |> Seq.length
    count = length

let pay_back (vm:VendingMachine)  = 
    printMethod ()
    vm.PayBack()

let sut () = 
    let vm = new VendingMachine(new StandardMoneyAcceptor())
    vm.SetDrinkSpecification([new PriceSpecification("Cola",110); new PriceSpecification("Soda",150)])
    vm

//Feature:お金が投入できる
[<Scenario>]
let ``After inserting 10 yen, it's total amount is 10``() =
  Given sut ()         
    |> When insert_money [new Money(MoneyKind.Yen10)]      
    |> It should have (total_amount 10)
    |> Verify

[<Scenario>]
let ``After inserting 10 yen and 100 yen, it's total amount is 110``() =
  Given sut ()        
    |> When insert_money [new Money(MoneyKind.Yen10);new Money(MoneyKind.Yen100)]  
    |> It should have (total_amount 110)
    |> Verify
                 
   
//Feature:投入金額の確認ができる
[<Scenario>]
let ``Initially, the total amount of this vending machine is 0``() =
  Given sut ()              
    |> When initially      
    |> It should have (total_amount 0)
    |> Verify


//Feature:扱えないお金を管理できる
[<Scenario>]
let ``After inserting 1 yen, it's invalid so machine's total amout is 0``() =
  Given sut ()              
    |> When insert_money [new Money(MoneyKind.Yen1)]      
    |> It should have (total_amount 0)
    |> Verify

//Feature：ジュースを購入する
[<Scenario>]
let ``After inserting 110 yen, you can buy a juice less than 110 yen``() =
       Given sut ()
            |> stocked standardStock
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen10]
            |> pushed [cola]
        |> When buy_drink_named "Cola"
        |> It should have (drink_named "Cola")
        |> Verify

//お金が足りずにジュースを購入できない場合
[<Scenario>]
let ``After inserting 100 yen, you can't buy a juice over than 100 yen``() =
    Given sut ()
            |> inserted [MoneyKind.Yen100;]
            |> pushed [cola]
        |> When buy_drink_named "Cola"
        |> It should equal null
        |> Verify

//在庫が足りずにジュースを購入できない場合
[<Scenario>]
let ``If there is no stock for supecified drink, you can't buy it``() =
    Given sut ()
            |> inserted [MoneyKind.Yen100;]
            |> pushed [cola]
        |> When buy_drink_named "Soda"
        |> It should equal null
        |> Verify


//Feature：購入後の払い戻し


[<Scenario>]
let ``After inserted 200yen and then bought drink costed 110yen, you can pay back 90 yen``() =
    Given sut ()
            |> stocked standardStock
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> pushed [cola]
            |> bought ["Cola"]
        |> When pay_back
        |> It should have (coin_50_yen_for 1)
        |> It should have (coin_10_yen_for 4)
        |> Verify


[<Scenario>]
let ``After inserted 200yen and then bought drink costed 150yen, you can pay back 50 yen``() =
    Given sut ()
            |> stocked standardStock
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> pushed [soda]
            |> bought ["Soda"]
        |> When pay_back
        |> It should have (length 1)
        |> It should have (coin_50_yen_for 1)
        |> Verify



//つり銭ストックが足りない場合、購入ができない
[<Scenario>]
let ``If vending machine doesn't have enough stock for change, you can't buy drink``() = 
    Given sut ()
            |> stocked [for i in 1 .. 5 -> MoneyKind.Yen10]
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> pushed[cola]
        |> When buy_drink_named "Cola"
        |> It should equal null
        |> Verify



//一つストックしておいて、同じものを二つ買う
[<Scenario>]
let ``If vending machine stocked only one drink and you try to buy two, you can't buy second one``() = 
    Given sut ()
        |> stocked standardStock
        |> inserted [MoneyKind.Yen100;MoneyKind.Yen100;MoneyKind.Yen100]
        |> pushed[cola]
        |> bought["Cola"]
    |> When buy_drink_named "Cola"
    |> It should equal null
    |> Verify