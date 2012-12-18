package net.codersation.vendingmachine.stock;

public class Juice {
    private final String juiceName;
    private final int price;

    Juice(String name, int price) {
        this.juiceName = name;
        this.price = price;
    }

    public static Juice create(String name) {
        switch (name) {
            case "コーラ":
                return new Juice("コーラ", 120);
            case "水":
                return new Juice("水", 100);
            case "レッドブル":
                return new Juice("レッドブル", 200);
            default:
                throw new IllegalArgumentException(name);
        }
    }

    public String getName() {
        return juiceName;
    }

    public int getPrice() {
        return price;
    }

    public boolean isEnough(int amount) {
        return amount >= this.getPrice();
    }

    @Override
    public int hashCode() {
        final int prime = 31;
        int result = 1;
        result = prime * result + ((juiceName == null) ? 0 : juiceName.hashCode());
        result = prime * result + price;
        return result;
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj)
            return true;
        if (obj == null)
            return false;
        if (getClass() != obj.getClass())
            return false;
        Juice other = (Juice) obj;
        if (juiceName == null) {
            if (other.juiceName != null)
                return false;
        } else if (!juiceName.equals(other.juiceName))
            return false;
        if (price != other.price)
            return false;
        return true;
    }
}
