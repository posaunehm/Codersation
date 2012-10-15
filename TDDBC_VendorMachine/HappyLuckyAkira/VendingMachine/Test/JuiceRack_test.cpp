/*
 * JuiceRack_test.cpp
 *
 *  Created on: 2012/10/13
 *      Author: Akira
 */

#include "../Product/JuiceRack.h"
#include "../Product/Juice.h"

#include "CppUTest/TestHarness.h"

TEST_GROUP(TestSuiteJuiceRack)
{
  void setup()
  {
    pJuiceRack_ = new JuiceRack(Juice::Coke(),5);
  }
  void teardown()
  {
    delete pJuiceRack_;
  }
protected:
  JuiceRack *pJuiceRack_;
};

//コーラを登録したらコーラを取得できる
TEST(TestSuiteJuiceRack, TestCaseGetCoke)
{
  bool IsEqual = false;
  Juice juiceTarget = pJuiceRack_->getJuice();
  if (juiceTarget==Juice::Coke()){
      IsEqual = true;
  }
  CHECK_TRUE(IsEqual);
//ToDo  CHECK_EQUAL(juiceTarget,(Juice::Coke()));で、いけるはずなんですが・・・
}

