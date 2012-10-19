/*
 * JuiceRack.cpp
 *
 *  Created on: 2012/10/13
 *      Author: Akira
 */

#include "JuiceRack.h"
#include "Juice.h"

//こちらが、普段使いのコンストラクタ
JuiceRack::JuiceRack(const Juice& juice, int count):count_(count)
{
  //ToDo：強い保証をするなら、これでは不十分。例外をとり、ダメなら0に して、例外を戻す
  pJuice_      = new Juice(juice);
}

void
JuiceRack::remove(void)
{
}

bool
JuiceRack::Of(const Juice& juice)
{
  if (*pJuice_ == juice){return true;}
  return false;
}

//C++11だと、delete
JuiceRack::JuiceRack():pJuice_(0),count_(0)
{
}

JuiceRack::~JuiceRack()
{
  //delete 書いたら負け。負け確定
  //ToDo：shared_ptrに変更
  delete pJuice_;
}
//0になった時点で、このJuiceRackには、+しかできなくなるような工夫が欲しい。
//それが無理なら、0になったら一旦登録から外す方が良いのかもね。
//とりあえずは、在庫ありかを判定できるようなインターフェイス入れとく

