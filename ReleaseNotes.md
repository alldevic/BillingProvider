# Billing Provider 0.3.40

## Важно

*Требуется обновление всех файлов, даже настроек BillingProvider.config*

## Изменения

- Общее: добавлена поддержка KkmServer
- Общее: реаизовано скрытие неиспользуемых настроек в зависимости от выбранного драйвера
- Общее: исправлена проблема с игнорироанием выбранного драйвера при сохранении настроек
- Общее: во избежание случайных нажатий пункты меню с неиспользуемым функционалом деактивированы
- KkmServer: реализована поддержка НДС из глобальных настроек, способ расчета и признак способа расчета - для каждого чека
- KkmServer: тестовый чек по-прежнему фискальный, но теперь на сумму 0,00
- KkmServer: реализована поддержка способа расчета и признака способа расчета для файлов
- Atol: произведена внутренняя оптимизация для подготовки к поддержке функционала из KkmServer
- Atol: добавлена проверка связи с сервером (пробуем получить api key)
