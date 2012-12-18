/*
 * MoneyStock_test.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "../Product/MoneyStock.h"

#include "CppUTest/TestHarness.h"

TEST_GROUP(TestSuiteMoneyStock)
{
  void setup()
  {
    pMoneyStock_ = new MoneyStock(Money::Yen10(),50);
  }
  void teardown()
  {
    delete pMoneyStock_;
  }
protected:
  MoneyStock*   pMoneyStock_;
};
//500円のラックと、10円のラックを登録後、500円の枚数が取得できる
TEST(TestSuiteMoneyStock, TestCaseGetYen500Count)
{

   LONGS_EQUAL(500,pMoneyStock_->getAmount());
}

//登録されていないお金の枚数は0が返ってくる。
//登録されているお金の合計が分かる。


