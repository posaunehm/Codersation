/*
 * Money.cpp
 *
 *  Created on: 2012/09/30
 *      Author: Akira
 */

#include "Money.h"

Money::~Money()
{

}
Money::Money(int money)
  :val_(money)
{

}
bool Money::operator==(const Money& money)
{
  if (this->val_ == money.val_){ return true ;}
  return false;

}

//使えるお金かを確認する。
bool IsValidMoney(Money money)
{
  if (money == Money::Yen10()   ||
      money == Money::Yen100()  ||
      money == Money::Yen500()  ||
      money == Money::Yen1000()){
      return true;
  }
  return false;
}
