/*
 * MoneyRack.cpp
 *
 *  Created on: 2012/10/25
 *      Author: Akira
 */

#include "MoneyRack.h"
#include "Money.h"
#include <boost/make_shared.hpp>

//ここまでしなくても良いかもしれないが、将来的には、shared_pointerのラッピングでいけるようにできる気がする
//強い保証は、できていない。
//countが0になったら、自動的に削除するという設計が良さそう
MoneyRack::MoneyRack(const Money& money, int count)
{
  //強い保証をするには、ここで例外を受けるようにしとけばいける。
  pMoney_ = MoneyPointer(new Money(money));
  count_ = count;

}

MoneyRack
MoneyRack::operator +(const Money& rmoney)
{
  MoneyRack tmp(*this);
  Money money = *pMoney_;
  if (money == rmoney){++ tmp.count_;};
  return tmp;
}

MoneyRack&
MoneyRack::operator +=(const Money& rmoney)
{
  if (count_<= 0){return *this;};
  Money money = *pMoney_;
  if (money == rmoney){++count_;};
  return *this;
}

MoneyRack
MoneyRack::operator -(const Money& rmoney)
{
  MoneyRack tmp(*this);
  Money money = *pMoney_;
  if (money == rmoney){-- tmp.count_;};
  return tmp;
}

MoneyRack&
MoneyRack::operator -=(const Money& rmoney)
{
  if (count_<= 0){return *this;};
  Money money = *pMoney_;
  if (money == rmoney){--count_;};
  return *this;
}

MoneyRack::MoneyRack()
{
  std::cout << "デフォルトコンストラクタが呼び出された" << std::endl;
  pMoney_ = boost::shared_ptr<Money>();
  count_ = 0;

}

MoneyRack::~MoneyRack()
{
  // TODO Auto-generated destructor stub
}

