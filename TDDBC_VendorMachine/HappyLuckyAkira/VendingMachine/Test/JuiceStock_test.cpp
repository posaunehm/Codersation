/*
 * JuiceStock_test.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "../Product/JuiceStock.h"
#include "../Product/Juice.h"

#include "CppUTest/TestHarness.h"

TEST_GROUP(TestSuiteJuiceStock)
{
  void setup()
  {
     pJuiceStock_ = new JuiceStock(Juice::Coke(),5);
  }
  void teardown()
  {
  }
protected:
  JuiceStock *pJuiceStock_;

};

//コーラを登録したらコーラがストックされている
TEST(TestSuiteJuiceStock, TestCaseGetCoke)
{
  bool IsInStock = false;
  IsInStock = pJuiceStock_->IsInStock(Juice::Coke());
  CHECK_TRUE(IsInStock);
}
