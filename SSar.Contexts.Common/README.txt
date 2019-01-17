SSAR.CONTEXTS.COMMON PROJECT README

While this solution is currently a "monolith" due to the single distribution package and mostly synchronous 
operation, this project is carefully organized to prevent coupling between the bounded contexts. However,
to save development effort some universally-used code has been shared in a "common" project.

Keeping in mind that this program may be broken up into microservices in the future, and for ease of maintenance, the 
code in the common folder is organized into folders that are candidates to be broken into pacakges and distrubuted
in the by a package manager such as NuGet.

As code is added to the application please respect the lack of coupling between bounded contexts, communicate between
BCs only with events and keep common code modular for future refatoring into distributable packages.