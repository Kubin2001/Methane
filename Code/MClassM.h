#pragma once
#include <iostream>
#include <SDL.h>


class MClassM
{
    private:
        SDL_Renderer* renderer;
        SDL_Texture * texture = nullptr;
        SDL_Rect rectangle;

    public:
        MClassM(SDL_Renderer* renderer);

        SDL_Texture *GetTexture();

        void SetTexture(SDL_Texture* temptex);

        SDL_Rect* GetRectangle();

        void Render();

        ~MClassM();
};
