/*
 * VendingMachine.cpp
 *
 *  Created on: 2012/08/25
 *      Author: datsuns
 */

#include "VendingMachine.h"

VendingMachine::VendingMachine() :amount_(0)
{
}

VendingMachine::~VendingMachine()
{
}
int VendingMachine::GetAmount()
{ return amount_; }

int VendingMachine::InsertMoney( int money )
{
        if ( isValidMoney(money) )
        {
                amount_+=money;
                return 0;
        }
        return money;
}
int VendingMachine::PayBack()
{
        int payBackMoney = amount_;
        amount_ = 0;
        return payBackMoney;
};
std::string VendingMachine::GetJuiceName(void)
{
        return "Coke";
}
int VendingMachine::GetJuicePrice(void)
{
        return JUICE_PRICE;
}
int VendingMachine::GetJuiceStock(void)
{
        return 5;
}
int VendingMachine::GetSaleAmount(void)
{
        return 0;
}
bool VendingMachine::CanPurchase(void)
{
        if( amount_ >= JUICE_PRICE )
                return true;
        return false;
}
bool VendingMachine::isValidMoney(int money)
{
  if (money == 10 ||money == 50 || money == 100||money == 500||money == 1000){
          return true;
  }
  return false;
}
