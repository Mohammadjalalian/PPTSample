import {  useState } from 'react';

import './App.css';

function App() {
    const [userIdentifier, setUserIdentifier] = useState("");
    const [imageUrl, setImageUrl] = useState("https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150");


   
    const onClick = async () => {
        const response = await fetch(`https://localhost:7078/api/image/${userIdentifier}`);
        const responseBody = await response.json();
        console.log(responseBody.imageUrl);
        setImageUrl(responseBody.imageUrl);

    }
    return (
        <>
            <h1>Technical Test.</h1>
            <p>Change the user identifier to get their profile image</p>
            <div className="d-flex justify-content-center pb-3">
                <input type="text" className="form-control w-auto mx-2" value={userIdentifier} onChange={(value)=>{
                    console.log(value.target.value);
                    setUserIdentifier(value.target.value)
                }
                     } placeholder="User identifier"/>
                    <button className="btn btn-sm fw-bold border-white bg-white text-black mx-2" onClick={onClick}>Get profile image</button>
            </div>
            <div>
                <img src={ imageUrl} id="userAvatarImage" className="rounded border border-4 border-secondary" />
            </div>
          </>
        

    );
    
   
}

export default App;