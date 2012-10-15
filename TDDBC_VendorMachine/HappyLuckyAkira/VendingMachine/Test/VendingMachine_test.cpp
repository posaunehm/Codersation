#include "CppUTest/TestHarness.h"


#include <string>

#include "../Product/VendingMachine.h"

TEST_GROUP(TestSuiteVendingMachine)
{
  VendingMachine *pVendingMachine_;
  void setup()
  {
    pVendingMachine_ = new VendingMachine();
  }
  void teardown()
  {
    delete pVendingMachine_;
  }
};

//何もしなければ0
TEST(TestSuiteVendingMachine, TestInitAmount)
{
	int amount;
	amount = pVendingMachine_->GetAmount();
	LONGS_EQUAL(0, amount);
}
TEST( TestSuiteVendingMachine, TestInsert1000AND10Yen ){
	pVendingMachine_->InsertMoney( 1000 );
	pVendingMachine_->InsertMoney( 10 );

	int amount;
	amount = pVendingMachine_->GetAmount();
	LONGS_EQUAL(1010, amount);
}
TEST( TestSuiteVendingMachine,TestPayBack1010 ){
	pVendingMachine_->InsertMoney( 1000 );
	pVendingMachine_->InsertMoney( 10 );

	int paybackMoney;
	paybackMoney = pVendingMachine_->PayBack();
	LONGS_EQUAL(1010, paybackMoney);
}

TEST( TestSuiteVendingMachine,TestAmountClear ){
	pVendingMachine_->InsertMoney( 1000 );
	pVendingMachine_->InsertMoney( 10 );

	int paybackMoney;
	paybackMoney = pVendingMachine_->PayBack();

	int amount;
	amount = pVendingMachine_->GetAmount();
	LONGS_EQUAL(0, amount);
}
TEST( TestSuiteVendingMachine,TestChangeValidMoney ){

	int changeMoney;
	changeMoney = pVendingMachine_->InsertMoney(100);
	LONGS_EQUAL(0, changeMoney);
}
TEST( TestSuiteVendingMachine,TestChangeInvalidMoney ){

	int changeMoney;
	changeMoney = pVendingMachine_->InsertMoney(5);
	LONGS_EQUAL(5, changeMoney);
}
TEST( TestSuiteVendingMachine,TestInvalidMoneyCheckAmount ){

	int changeMoney;
	changeMoney = pVendingMachine_->InsertMoney(10000);

	int amount;
	amount = pVendingMachine_->GetAmount();
	LONGS_EQUAL(0, amount);
}
TEST( TestSuiteVendingMachine,TestPrevInsetMoney ){

	int changeMoney;
	changeMoney = pVendingMachine_->InsertMoney(100);

	changeMoney = pVendingMachine_->InsertMoney(10000);

	LONGS_EQUAL(10000, changeMoney);
}

TEST( TestSuiteVendingMachine,TestPrevInsetMoneyCheckAmount){

	int changeMoney;
	changeMoney = pVendingMachine_->InsertMoney(100);

	changeMoney = pVendingMachine_->InsertMoney(10000);

	int amount;
	amount = pVendingMachine_->GetAmount();
	LONGS_EQUAL(100, amount);
}
TEST( TestSuiteVendingMachine,TestGetJuiceName){
	std::string juicename;
	juicename = pVendingMachine_->GetJuiceName();
	STRCMP_EQUAL(juicename.c_str(), "Coke");
}
TEST( TestSuiteVendingMachine,TestGetJuicePrice){
	int juiceprice;
	juiceprice = pVendingMachine_->GetJuicePrice();
	LONGS_EQUAL(juiceprice, JUICE_PRICE);
}
TEST( TestSuiteVendingMachine,TestGetJuiceStock){
	int juicestock;
	juicestock = pVendingMachine_->GetJuiceStock();
	LONGS_EQUAL(juicestock, 5);
}
TEST( TestSuiteVendingMachine,TestGetInitSaleAmount){
	int saleamount;
	saleamount = pVendingMachine_->GetSaleAmount();
	LONGS_EQUAL(saleamount, 0);
}
TEST( TestSuiteVendingMachine,TestCanPurchase){
	bool canpurchase;
	canpurchase = pVendingMachine_->CanPurchase();
	LONGS_EQUAL(canpurchase, false);
}
TEST( TestSuiteVendingMachine,TestCanPurchaseMoney ){

	int changeMoney;
	changeMoney = pVendingMachine_->InsertMoney(500);

	bool canpurchase;
	canpurchase = pVendingMachine_->CanPurchase();

	LONGS_EQUAL(canpurchase, true);
}
