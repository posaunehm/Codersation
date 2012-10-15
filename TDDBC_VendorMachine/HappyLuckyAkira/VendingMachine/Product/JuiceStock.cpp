/*
 * JuiceStock.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "JuiceStock.h"
#include "Juice.h"

//これをコンストラクタとする
JuiceStock::JuiceStock(const Juice& juice, int count)
:count_(count)
{
  pJuice_ = new Juice(juice);
}

//C++11のインストールできないから、使わないけどデフォルトコンストラクタの記載しておく
JuiceStock::JuiceStock()
:pJuice_(0),count_(0)
{

}

JuiceStock::~JuiceStock()
{
  delete pJuice_;
}
//Operator+とかを考えたが、例外を返すとか、今ひとつ使いにくそうなので、上のJuiceStockで実装する方針とする。

