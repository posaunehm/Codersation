/*
 * JuiceStock.cpp
 *
 *  Created on: 2012/10/08
 *      Author: Akira
 */

#include "JuiceStock.h"
#include "Juice.h"
#include "JuiceRack.h"
#include <boost/make_shared.hpp>

//これをコンストラクタとする
JuiceStock::JuiceStock(const Juice& juice, int count)
{
  boost::shared_ptr<JuiceRack> d = boost::make_shared<JuiceRack>(juice,count);

}

bool
JuiceStock::IsInStock(const Juice& juice)
{

  return false;
}

//C++11のインストールできないから、使わないけどデフォルトコンストラクタの記載しておく
JuiceStock::JuiceStock()
{

}

JuiceStock::~JuiceStock()
{
}

