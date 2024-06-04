#include <iostream>
#include "MClassM.h"
int windowWidth = 1400;
int windowHeight = 800;
long long int framesCounter = 0;

int main(int argv, char* argc[])
{
    bool status = true;
    MClassM* mclassm = new MClassM();

    mclassm->Start();

    while (status)
    {
        mclassm->Movement(status);
        mclassm->Events();
        mclassm->Render();

        SDL_Delay(16);
    }

    delete mclassm;
    return 0;
}
