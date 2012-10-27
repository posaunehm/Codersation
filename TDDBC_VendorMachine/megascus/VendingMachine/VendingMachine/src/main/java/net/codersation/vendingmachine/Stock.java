/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.codersation.vendingmachine;

import java.util.Objects;

/**
 * ひとつの商品の在庫を管理します。
 * @author megascus
 */
public class Stock <T extends Product> {
    private T product;
    private int price;
    private int count;

    public Stock(T product, int price, int count) {
        this.product = product;
        this.price = price;
        this.count = count;
    }
    
    public T getProduct() {
        return this.product;
    }

    public int getPrice() {
        return price;
    }

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
    }
    
    /**
     * 在庫を一つ減らします。
     */
    public void decrement() {
        count--;
    }

    @Override
    public int hashCode() {
        int hash = 7;
        hash = 67 * hash + Objects.hashCode(this.product);
        hash = 67 * hash + this.price;
        hash = 67 * hash + this.count;
        return hash;
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final Stock<T> other = (Stock<T>) obj;
        if (!Objects.equals(this.product, other.product)) {
            return false;
        }
        if (this.price != other.price) {
            return false;
        }
        if (this.count != other.count) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "Stock{" + "product=" + product + ", price=" + price + ", count=" + count + '}';
    }
    
    


}
