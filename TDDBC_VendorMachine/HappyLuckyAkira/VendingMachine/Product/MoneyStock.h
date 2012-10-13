/*
 * MoneyStock.h
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#ifndef MONEYSTOCK_H_
#define MONEYSTOCK_H_

#include "Money.h"
class Money;

class MoneyStock
{
public:
  virtual
  ~MoneyStock();
  MoneyStock(Money money,int count);

  int
  getCount() const
  {
    return count_;
  }

  const Money&
  getMoney() const
  {
    return *pMoney_;
  }
private:
  MoneyStock();
  Money* pMoney_;
  int count_;
};

#endif /* MONEYSTOCK_H_ */
