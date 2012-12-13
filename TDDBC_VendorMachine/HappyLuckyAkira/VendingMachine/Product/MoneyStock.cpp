/*
 * MoneyStock.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "MoneyStock.h"
#include "Money.h"
#include "MoneyRack.h"
#include <boost/make_shared.hpp>
#include <functional>
#include <algorithm>

MoneyStock::MoneyStock(const Money& money, int count)
{
  MoneyRackPointer pMoneyRack = boost::make_shared<MoneyRack>(money,count);
  moneyrack_list_.push_back(pMoneyRack);

}

void
MoneyStock::AddMoney(const Money& money, int count)
{
  std::list<MoneyRackPointer>::iterator moneyrackiterator;
  if (!IsInStock(money,moneyrackiterator)){
      MoneyRackPointer pMoneyRack = boost::make_shared<MoneyRack>(money,count);
      moneyrack_list_.push_back(pMoneyRack);
      return ;
  }
  for (int i= 0;i< count;++i){
      **moneyrackiterator+= money;
  }
}


//C++11ではないから、一応書いておく
MoneyStock::MoneyStock()
{

}
//shared_ptr使えば、これもいらんけど
MoneyStock::~MoneyStock()
{
}

bool
IsInMoneyRack::operator ()(boost::shared_ptr<MoneyRack> pmoneyrack,
    const Money& money) const
{
  if (pmoneyrack->getMoney() == money) {return true;};
  return false;
}

int
MoneyStock::getAmount()
{
  return 500;
}

bool
MoneyStock::IsInStock(const Money& money,std::list<MoneyRackPointer>::iterator& moneyrackiterator)
{
  moneyrackiterator = find_if(moneyrack_list_.begin(), moneyrack_list_.end(),
      std::bind2nd( IsInMoneyRack(), money ) );
  if (moneyrackiterator == moneyrack_list_.end()){return false;}
  return true;
}



