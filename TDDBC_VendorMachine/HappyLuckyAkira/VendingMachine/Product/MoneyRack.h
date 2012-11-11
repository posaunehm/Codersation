/*
 * MoneyRack.h
 *
 *  Created on: 2012/10/25
 *      Author: Akira
 */

#ifndef MONEYRACK_H_
#define MONEYRACK_H_
#include <boost/shared_ptr.hpp>
class Money;

class MoneyRack
{
public:
  MoneyRack(const Money& money,int count);
  virtual
  ~MoneyRack();
  MoneyRack operator+(const Money& rmoney);
  MoneyRack& operator+=(const Money& rmoney);
  MoneyRack operator-(const Money& rmoney);
  MoneyRack& operator-=(const Money& rmoney);

  int
  getCount() const
  {
    return count_;
  }

  Money&
  getMoney() const
  {
    return *pMoney_;
  }
private:
  typedef boost::shared_ptr<Money> MoneyPointer;
  MoneyRack();
  MoneyPointer pMoney_;
  int count_;
};

#endif /* MONEYRACK_H_ */
