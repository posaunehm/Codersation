/*
 * Juice_test.cpp
 *
 *  Created on: 2012/10/07
 *      Author: Akira
 */

#include "../Product/Juice.h"

#include "CppUTest/TestHarness.h"

TEST_GROUP(TestSuiteJuice)
{
  void setup()
  {
  }
  void teardown()
  {
  }
};


//Cokeの名前はCoke
TEST(TestSuiteJuice, TestCaseCokeName)
{
  Juice coke = Juice::Coke();
  std::string name = coke.getName();
  STRCMP_EQUAL(name.c_str(),"Coke");
}

//Cokeの値段は120円
TEST(TestSuiteJuice, TestCaseCokePrice)
{
  Juice coke = Juice::Coke();
  int price = coke.getPrice();
  LONGS_EQUAL(120,price);
}
