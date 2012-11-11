/*
 * MoneyRack_test.cpp
 *
 *  Created on: 2012/10/25
 *      Author: Akira
 */

#include "../Product/MoneyRack.h"
#include "../Product/Money.h"

#include "CppUTest/TestHarness.h"

TEST_GROUP(TestSuiteMoneyRack)
{
  void setup()
  {
    pMoneyYen100Rack_ = new MoneyRack(Money::Yen100(),50);
    pMoneyYen50Rack_  = new MoneyRack(Money::Yen50(),10);
    pMoneyYen10Rack_  = new MoneyRack(Money::Yen10(),10);
  }
  void teardown()
  {
    delete pMoneyYen100Rack_;
    delete pMoneyYen50Rack_;
    delete pMoneyYen10Rack_;
  }
protected:
  MoneyRack *pMoneyYen100Rack_;
  MoneyRack *pMoneyYen50Rack_;
  MoneyRack *pMoneyYen10Rack_;
};

//初期値をとったら初期値がとれる
TEST(TestSuiteMoneyRack, TestCaseGetInitCount)
{
  LONGS_EQUAL(50,pMoneyYen100Rack_->getCount());
}
//100円を認識できる
TEST(TestSuiteMoneyRack, TestCaseGetInitMoney)
{
  bool IsEqual = false;
  Money MoneyTarget = pMoneyYen100Rack_->getMoney();
  if (MoneyTarget==Money::Yen100()){
      IsEqual = true;
  }
  CHECK_TRUE(IsEqual);
}

//100円追加したら1枚増える
TEST(TestSuiteMoneyRack, TestCaseGetAddEqualCount)
{
  *pMoneyYen100Rack_ += Money::Yen100();
  LONGS_EQUAL(51,pMoneyYen100Rack_->getCount());
}
//異なる場合は増えない
TEST(TestSuiteMoneyRack, TestCaseGetAddErrCount)
{
  *pMoneyYen10Rack_ =  *pMoneyYen10Rack_ + Money::Yen100();
  LONGS_EQUAL(10,pMoneyYen10Rack_->getCount());
}

//10円を追加したら1枚増える（別の演算子）
TEST(TestSuiteMoneyRack, TestCaseGetAddCount)
{
  *pMoneyYen10Rack_ =  *pMoneyYen10Rack_ + Money::Yen10();
  LONGS_EQUAL(11,pMoneyYen10Rack_->getCount());
}

//10円を1枚使用したら1枚増える（別の演算子）
TEST(TestSuiteMoneyRack, TestCaseGetSubCount)
{
  *pMoneyYen50Rack_ =  *pMoneyYen50Rack_ - Money::Yen50();
  LONGS_EQUAL(9,pMoneyYen50Rack_->getCount());
}
