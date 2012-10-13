/*
 * MoneyStock.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "MoneyStock.h"
#include "Money.h"

MoneyStock::MoneyStock(Money money, int count)
:pMoney_(new Money(money)),count_(count)
{
}

//C++11ではないから、一応書いておく
MoneyStock::MoneyStock()
:pMoney_(0),count_(0)
{

}
//shared_ptr使えば、これもいらんけど
MoneyStock::~MoneyStock()
{
  delete pMoney_;
}

