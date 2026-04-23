[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/TaXko0kR)
# Wzorce: Pule obliczeń

| Termin oddania | Punkty     |
|----------------|:-----------|
| 24.04.2026 23:00|  10       |

--- 
Przekroczenie terminu o **n** zajęć wiąże się z karą:
- punkty uzyskania za realizację zadania są dzielone przez **2<sup>n</sup>**.

--- 
Utworzyć trzy pule obiektów reprezentujących trzy rodzaje konfigurowalnych 
   i skomplikowanych obliczeń podstawowych. 
   Obiekty puli zawierają stan reprezentujący zarówno stan konfiguracji, jak i obliczeń.
   - każda pula powinna umożliwiać współbieżne korzystanie ze swoich zasobów
    przez wiele współbieżnych wątków obliczających
   - każda pula powinna się automatycznie powiększać tak, by zoptymalizować 
     zarówno czas oczekiwania na dostępność obiektów w puli, jak i minimalizować
     liczbę obiektów w puli; 
     nowe obiekty w puli powinny być wytwarzane przy wykorzystaniu wzorca prototyp
   - każdy wątek obliczający jest tworzony z wykorzystaniem wzorca builder 
    i ustalamy w nim ile i jakich obiektów reprezentujących obliczenia 
     musi pobrać z puli (lub z wielu pul) i jak je skonfigurować
   - po skompletowaniu wszystkich niezbędnych obiektów z puli / z pul
     wątek wykonuje obliczenia, po czym zwraca obiekty do puli / pul i się kończy.

Czy w opisanym powyżej problemie możemy uzyskać jakieś przewagi, jeżeli zastosujemy fabryki? 
Jeżeli tak/nie, przedstawić pomiary/testy potwierdzające.

Zbadać optymalność algorytmu zarządzania elementami puli oraz 
zależność liczby wątków obliczających ich złożoności obliczeniowej i 
liczby niezbędnych obiektów w pulach.

Do zaproponowanych rozwiązań załączyć odpowiednie testy wydajnościowe / funkcjonalne / jednostkowe.
Można używać narzędzi do profilowania kodu dedykowanych danej technologii. 
