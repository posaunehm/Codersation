/*
 * MoneyStock.h
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#ifndef MONEYSTOCK_H_
#define MONEYSTOCK_H_

#include "Money.h"
#include "MoneyRack.h"
#include <list>
#include <boost/shared_ptr.hpp>

//class MoneyRack;

class MoneyStock
{
public:
  virtual
  ~MoneyStock();
  MoneyStock(const Money& money,int count);
  void AddMoney(const Money& money,int count);
  int getAmount();
private:
  MoneyStock();
  typedef boost::shared_ptr<MoneyRack> MoneyRackPointer;
  bool IsInStock(const Money& money,std::list<MoneyRackPointer>::iterator& moneyrackiterator);
  std::list<MoneyRackPointer> moneyrack_list_;
};
class IsInMoneyRack : public std::binary_function<boost::shared_ptr<MoneyRack>,Money,bool> {
public:
  bool operator()(boost::shared_ptr<MoneyRack> pmoneyrack, const Money& money) const;
 };

#endif /* MONEYSTOCK_H_ */
