#pragma once
#include <iostream>
#include <SDL.h>


class MClassM
{
    private:
        SDL_Renderer* renderer;
        SDL_Texture * texture = nullptr;

    public:
        MClassM(SDL_Renderer* renderer);

        SDL_Texture *GetTexture();

        void SetTexture(SDL_Texture* temptex);

        void Render();

        ~MClassM();
};
