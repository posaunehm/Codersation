/*
 * Juice.h
 *
 *  Created on: 2012/10/07
 *      Author: Akira
 */

#ifndef JUICE_H_
#define JUICE_H_

#include <string>

class Juice
{
public:
    virtual ~Juice();
    explicit Juice(const std::string& name, int price);

    std::string getName() const
    {
        return name_;
    }

    int getPrice() const
    {
        return price_;
    }
    static Juice Coke()     {return Juice("Coke",120);}
    bool operator==(const Juice& rjuice) const;
    bool operator!=(const Juice& rjuice) const;
private:
    Juice();
    std::string name_;
    int price_;
};

#endif /* JUICE_H_ */
