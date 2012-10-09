module Spec.VendingMachineSpecification

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
 
//Feature:お金が投入できる
[<Scenario>]
let ``After inserting 10 yen, it's total amount is 10``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))          
    |> When insert_money [new Money(MoneyKind.Yen10)]      
    |> It should have (total_amount 10)
    |> Verify

[<Scenario>]
let ``After inserting 10 yen and 100 yen, it's total amount is 110``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))          
    |> When insert_money [new Money(MoneyKind.Yen10);new Money(MoneyKind.Yen100)]  
    |> It should have (total_amount 110)
    |> Verify
                 
   
//Feature:投入金額の確認ができる
[<Scenario>]
let ``Initially, the total amount of this vending machine is 0``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))                
    |> When initially      
    |> It should have (total_amount 0)
    |> Verify


//Feature:扱えないお金を管理できる
[<Scenario>]
let ``After inserting 1 yen, it's invalid so machine's total amout is 0``() =
  Given (new VendingMachine(new StandardMoneyAcceptor()))                
    |> When insert_money [new Money(MoneyKind.Yen1)]      
    |> It should have (total_amount 0)
    |> Verify

//Feature：ジュースを購入する
let cola = new Drink("Cola", 110);

let vending_machine_inserted_110_yen_and_have_one_drink = 
    printMethod ()
    let vm = new VendingMachine(new StandardMoneyAcceptor())
    vm.AddDrink(cola)
    vm.InsertMoney(new Money(MoneyKind.Yen100))
    vm.InsertMoney(new Money(MoneyKind.Yen10))
    vm
    
let buy_drink drink (vm:VendingMachine) = 
    printMethod drink
    vm.BuyDrink(drink)


[<Scenario>]
let ``After inserting 110 yen, you can buy a juice less than 110 yen``() =
    Given vending_machine_inserted_110_yen_and_have_one_drink
        |> When buy_drink cola
        |> It should equal cola 
        |> Verify

//ジュースを購入できない場合
let vending_machine_inserted_100_yen_and_have_one_drink = 
    printMethod ()
    let vm = new VendingMachine(new StandardMoneyAcceptor())
    vm.AddDrink(cola)
    vm.InsertMoney(new Money(MoneyKind.Yen100))
    vm

[<Scenario>]
let ``After inserting 100 yen, you can't buy a juice over than 100 yen``() =
    Given vending_machine_inserted_100_yen_and_have_one_drink
        |> When buy_drink cola
        |> It should equal null
        |> Verify


//Feature：購入後の払い戻し
let vending_machine_inserted_200_yen_and_bought_110yen_drink = 
    printMethod ()
    let vm = new VendingMachine(new StandardMoneyAcceptor())
    vm.AddDrink(cola)
    vm.InsertMoney(new Money(MoneyKind.Yen100))
    vm.InsertMoney(new Money(MoneyKind.Yen100))
    vm.BuyDrink(cola)
    vm

let vending_machine_inserted_200_yen_and_bought_150yen_drink = 
    printMethod ()
    let vm = new VendingMachine(new StandardMoneyAcceptor())
    let soda = new Drink("Soda",150)
    vm.AddDrink(soda)
    vm.InsertMoney(new Money(MoneyKind.Yen100))
    vm.InsertMoney(new Money(MoneyKind.Yen100))
    vm.BuyDrink(soda)
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
    vm.PayBack().ToArray() |> Seq.ofArray

[<Scenario>]
let ``After inserted 200yen and then bought drink costed 110yen, you can pay back 90 yen``() =
    Given vending_machine_inserted_200_yen_and_bought_110yen_drink
        |> When pay_back
        |> It should have (coin_50_yen_for 1)
        |> It should have (coin_10_yen_for 4)
        |> Verify


[<Scenario>]
let ``After inserted 200yen and then bought drink costed 150yen, you can pay back 50 yen``() =
    Given vending_machine_inserted_200_yen_and_bought_150yen_drink
        |> When pay_back
        |> It should have (length 1)
        |> It should have (coin_50_yen_for 1)
        |> Verify


