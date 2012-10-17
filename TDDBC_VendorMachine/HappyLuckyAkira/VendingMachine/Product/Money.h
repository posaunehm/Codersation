/*
 * Money.h
 *
 *  Created on: 2012/09/30
 *      Author: Akira
 */

#ifndef MONEY_H_
#define MONEY_H_

class Money {
public:
  virtual ~Money();

  int
  getVal() const
  {
    return val_;
  }
  static Money Yen1()     {return Money(1);}
  static Money Yen5()     {return Money(5);}
  static Money Yen10()    {return Money(10);}
  static Money Yen50()    {return Money(50);}
  static Money Yen100()   {return Money(100);}
  static Money Yen500()   {return Money(500);}
  static Money Yen1000()  {return Money(1000);}
  static Money Yen2000()  {return Money(2000);}
  static Money Yen5000()  {return Money(5000);}
  static Money Yen10000() {return Money(10000);}
  bool operator==(const Money& rmoney) ;
private:
  explicit Money(int money);
  int val_;
};

bool IsValidMoney(Money money);

#endif /* MONEY_H_ */
