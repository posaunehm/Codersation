/*
 * Money_test.cpp
 *
 *  Created on: 2012/09/30
 *      Author: Akira
 */

#include "../Product/Money.h"

#include "CppUTest/TestHarness.h"


TEST_GROUP(TestSuiteMoney)
{
  void setup()
  {
  }
  void teardown()
  {
  }
};
//1円
TEST(TestSuiteMoney, TestCaseYen1)
{
    int checkvalue = Money::Yen1().getVal();
    LONGS_EQUAL(checkvalue,1);
}

//10円
TEST(TestSuiteMoney, TestCaseYen10)
{
    int checkvalue = Money::Yen10().getVal();
    LONGS_EQUAL(checkvalue,10);
}

//100円
TEST(TestSuiteMoney, TestCaseYen100)
{
    int checkvalue = Money::Yen100().getVal();
    LONGS_EQUAL( checkvalue,100);
}

//500円
TEST(TestSuiteMoney, TestCaseYen500)
{
    int checkvalue = Money::Yen500().getVal();
    LONGS_EQUAL(checkvalue,500);
}

//1000円
TEST(TestSuiteMoney, TestCaseYen1000)
{
    int checkvalue = Money::Yen1000().getVal();
    LONGS_EQUAL(checkvalue,1000);
}

//10円は使える。
TEST(TestSuiteMoney, TestCaseCanUse10Yen)
{
    bool canUse = IsValidMoney(Money::Yen10());
    CHECK_TRUE(canUse);
}
//1円は使えない
TEST(TestSuiteMoney, TestCaseCantUse1Yen)
{
    bool canUse = IsValidMoney(Money::Yen1());
    CHECK_FALSE(canUse);
}
//1000円は使える。
TEST(TestSuiteMoney, TestCaseCanUse1000Yen)
{
    bool canUse = IsValidMoney(Money::Yen1000());
    CHECK_TRUE(canUse);
}

